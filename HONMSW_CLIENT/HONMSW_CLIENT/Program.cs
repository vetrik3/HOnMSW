using Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HONMSW_CLIENT;

namespace HONMSW_CLIENT
{
    class Program
    {
        static UdpClient ReceivingSocket;
        static IPEndPoint ReceivingIEP;

        static string MulticastAddress;
        static int Port;
        static int ReceivingPort;

        static List<string> ComputerDetails = new List<string>();

        static UdpClient TASK_ReceivingSocket;
        static IPEndPoint TASK_ReceivingIEP;

        static string TASK_MulticastAddress;
        static int TASK_Port;
        static int TASK_ReceivingPort;
        static Thread TASK_Thread;
        static UDP_MULTICAST UDP_CLIENT;
        static List<string> Commands = new List<string>();

        static string MacAddress;
        static string Info_data;

        static bool ConnectToAnotherAddress;
        static bool connected;
        static bool creating;
        static bool accept;

        static int WAITING_TIME;

        const int ERROR_MESSAGE = -4;
        const int INFO_MESSAGE = -1;
        const int ACK_FLAG = 1;
        const int SYN_FLAG = 2;
        const int CONNECT_MESSAGE = 3;
        const int ACK_CONNECT_MESSAGE = 4;
        const int TURN_OFF_MESSAGE = 5;
        const int ACK_TURN_OFF = 6;
        const int FINISH_MESSAGE = 7;
        const int ACK_FINISH = 8;        
        const int CONNECTION_FLAG = 10;
        const int UDP_MULTICAST_MESSAGE = 11;
        const int TCP_UNICAST_MESSAGE = 13;
        const int ACK_TCP_UNICAST = 14;
        const int RUN_COMMAND = 15;
        const int ACK_RUN_COMMAND = 16;
        const int FINISH_RUN_COMMAND = 17;
        const int ACK_FINISH_RUN_COMMAND = 18;
        

        static void UDP_IncomingData(IAsyncResult ar)
        {
            try
            {
                byte[] bResp = ReceivingSocket.EndReceive(ar, ref ReceivingIEP);

                FilePiece piece = FilePiece.parse_packet(bResp);

                switch (piece.state)
                {
                    case ACK_FLAG:
                        {
                            string paket_string_data = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            if (MacAddress == paket_string_data.Split('#')[1])
                            {
                                Console.WriteLine("Acknowledgment message by: " + paket_string_data.Split('#')[0]);
                            }
                            break;
                        } 
                    case ERROR_MESSAGE:
                        {
                            string MacAddresses = Get_MacAddressFromPaket(piece);

                            List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                            if (ClientsMacs.Contains(MacAddress))
                            {
                                Console.WriteLine("ERROR MEESAGE");
                                ConnectToAnotherAddress = false;
                                connected = false;
                                if(TASK_Thread.IsAlive)
                                {
                                    TASK_ReceivingSocket.Close();
                                    TASK_Thread.Abort();
                                }
                            }
                            break;
                        }
                    case CONNECT_MESSAGE:
                        {
                            string MacAddresses_MulticastAddress = Get_MacAddressFromPaket(piece);

                            string MacAddresses = MacAddresses_MulticastAddress.Split(new string[] { "||" }, StringSplitOptions.None)[0];
                            
                            List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                            if (ClientsMacs.Contains(MacAddress))
                            {                                
                                Console.WriteLine("Connect to TASK");
                                TASK_DestroyConnection();
                               
                                ConnectToAnotherAddress = true;

                                TASK_MulticastAddress = MacAddresses_MulticastAddress.Split(new string[] { "||" }, StringSplitOptions.None)[1];

                                TASK_ReceivingPort = Convert.ToInt16(TASK_MulticastAddress.Split('.')[3]);
                                TASK_Port = 9000;
                                TASK_Thread = new Thread(new ThreadStart(CreateThreadForTask));
                                TASK_Thread.Start();
                            }                           
                            break;
                        }
                    default:
                        {
                            string paket_string_data = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);                           
                            break;
                        }
                }

                ReceivingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);
            }            
            catch
            {                
                CreateConnection();
            }
        }

        static string Get_MacAddressFromPaket(FilePiece paket)
        {
            string clientMacAdress = Encoding.ASCII.GetString(paket.data, 0, paket.data.Length);
            return clientMacAdress;
        }

        static string GetMacAddress()
        {
            string macAddresses = string.Empty;

            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    macAddresses += BitConverter.ToString(nic.GetPhysicalAddress().GetAddressBytes()).Replace('-', ':');
                    break;
                }
            }
            return macAddresses;
            //return "00:00:00:00:00:00";
        }

        static void SendMessage(short state, Int64 position, byte[] data)
        {
            try
            {
                FilePiece resend_piece = new FilePiece(state, position, data);
                byte[] bytes = resend_piece.get_packet();
                ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
            }
            catch { CreateConnection(); }
        }

        static void TASK_SendMessage(short state, Int64 position, byte[] data)
        {
            try
            {
                FilePiece resend_piece = new FilePiece(state, position, data);
                byte[] bytes = resend_piece.get_packet();
                TASK_ReceivingSocket.Send(bytes, bytes.Length, TASK_ReceivingIEP);
            }
            catch { }
        }

        static void TASK_CreateConnection()
        {
            if (!(creating))
            {
                creating = true;
                try
                {
                    DestroyConnection();
                }
                catch
                {
                }
                TASK_ReceivingSocket = new UdpClient(TASK_Port);

                TASK_ReceivingIEP = new IPEndPoint(IPAddress.Any, TASK_ReceivingPort);

                IPAddress multicastaddress = IPAddress.Parse(TASK_MulticastAddress);

                IPEndPoint ep = new IPEndPoint(multicastaddress, TASK_Port);

                TASK_ReceivingSocket.JoinMulticastGroup(multicastaddress);
                Thread.Sleep(100);
                creating = false;
            }
        }

        static void CreateThreadForTask()
        {
            TASK_CreateConnection();
            Receiving();
        }

        static void ChangeMacAddressInComputerDetails()
        {
            MacAddress = GetMacAddress();
            ComputerDetails[1] = "MacAddress||" + MacAddress;
        }

        static void SetComputerDetails()
        {            
            ComputerDetails = new List<string>();
            ComputerDetails.Add("Computer Name||" + System.Environment.MachineName);
            MacAddress = GetMacAddress();
            ComputerDetails.Add("MacAddress||" + MacAddress);
            ComputerDetails.Add(HardwareInfo.GetOSInformation());
            ComputerDetails.Add(HardwareInfo.GetProcessorInformation());
            ComputerDetails.Add(HardwareInfo.GetPhysicalMemory());
            ComputerDetails.Add(HardwareInfo.GetNoRamSlots());
            ComputerDetails.Add(HardwareInfo.GetBIOScaption());
            ComputerDetails.Add(HardwareInfo.GetBoardProductId());
            ComputerDetails.Add(HardwareInfo.GetAccountName());
        }

        static void UpdateStatus()
        {
            while (true)
            {
                WAITING_TIME = 10000;
                ChangeMacAddressInComputerDetails();
                var result_string = String.Join("|..|", ComputerDetails.ToArray());
                byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(result_string);
                SendMessage(SYN_FLAG, INFO_MESSAGE, Sending_Info_Data);
                Console.WriteLine("Sending Computer Details: " + ComputerDetails[0] + " " + ComputerDetails[1]);
                Thread.Sleep(WAITING_TIME);
            }
        }

        static void SendMessageWithMacAddress(short state)
        {
            MacAddress = GetMacAddress();
            byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(MacAddress);
            SendMessage(state, INFO_MESSAGE, Sending_Info_Data);
        }
        
        static void TASK_SendMessageWithMacAddress(short state)
        {
            MacAddress = GetMacAddress();
            byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(MacAddress);
            TASK_SendMessage(state, INFO_MESSAGE, Sending_Info_Data);
        }

        static void RunCommand(string FileName, string Arguments)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = FileName;
            proc.StartInfo.Arguments = Arguments;
            proc.Start();
            proc.WaitForExit();
            proc.Close();
        }

        static void WaitForACK(short state)
        {
            while(!(accept))
            {
                TASK_SendMessageWithMacAddress(state);
                Thread.Sleep(300);
            }
        }

        static void Receiving()
        {
            connected = true;
            Console.WriteLine("Connected");
            while (connected)
            {
                try
                {
                    byte[] paket = TASK_ReceivingSocket.Receive(ref TASK_ReceivingIEP);
                    FilePiece piece = FilePiece.parse_packet(paket);
                    switch (piece.state)
                    {
                        case ERROR_MESSAGE:
                            {
                                TASK_ReceivingSocket.Close();
                                TASK_Thread.Abort();
                                connected = false;
                                ConnectToAnotherAddress = false;
                                break;
                            }
                        case SYN_FLAG:
                            {
                                string MacAddresses = Get_MacAddressFromPaket(piece);
                                List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    Console.WriteLine("SYN_FLAG");
                                    TASK_SendMessageWithMacAddress(ACK_FLAG);
                                }
                                break;
                            }
                        case TCP_UNICAST_MESSAGE:
                            {
                                string paket_string_data = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                                if (MacAddress == paket_string_data.Split('#')[1])
                                {
                                    Console.WriteLine("TCP UNICAST MESSAGE");
                                    string hostName = Dns.GetHostName();
                                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                                    byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(myIP);
                                    TASK_SendMessage(ACK_TCP_UNICAST, INFO_MESSAGE, Sending_Info_Data);
                                    TCP_UNICAST TCP_CLIENT = new TCP_UNICAST();
                                    TCP_CLIENT.ServerPORT = TASK_ReceivingPort;
                                    TCP_CLIENT.ServerIPAddress = paket_string_data.Split('#')[0];
                                    TCP_CLIENT.ReceivingIEP = TASK_ReceivingIEP;
                                    TCP_CLIENT.ReceivingSocket = TASK_ReceivingSocket;
                                    TCP_CLIENT.WAITING_TIME = WAITING_TIME;
                                    TCP_CLIENT.TCP_UNICAST_RUN();
                                }

                                break;
                            }
                        case UDP_MULTICAST_MESSAGE:
                            {
                                UDP_CLIENT = new UDP_MULTICAST();
                                UDP_CLIENT.MacAddress = MacAddress;
                                UDP_CLIENT.ReceivingIEP = TASK_ReceivingIEP;
                                UDP_CLIENT.ReceivingSocket = TASK_ReceivingSocket;
                                UDP_CLIENT.WAITING_TIME = WAITING_TIME;
                                if (UDP_CLIENT.UDP_MULTICAST_RUN())
                                {
                                    connected = false;
                                    TASK_SendMessageWithMacAddress(ERROR_MESSAGE);
                                }
                                break;
                            }                            
                        case TURN_OFF_MESSAGE:
                            {
                                string MacAddresses = Get_MacAddressFromPaket(piece);
                                List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    Console.WriteLine("TURNOFF_FLAG");
                                    TASK_SendMessageWithMacAddress(ACK_TURN_OFF);
                                }
                                break;
                            }
                        case RUN_COMMAND:
                            {
                                string MacAddresses_COMMAND = Get_MacAddressFromPaket(piece);
                                string MacAddresses = MacAddresses_COMMAND.Split(new string[] { "||" }, StringSplitOptions.None)[0];
                            
                                List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    Console.WriteLine("RUNNING COMMAND");
                                    string Program_Name_with_Arguments = MacAddresses_COMMAND.Split(new string[] { "||" }, StringSplitOptions.None)[1];
                                    try
                                    {
                                        if (Program_Name_with_Arguments != "")
                                        {
                                            System.IO.FileInfo fi = null;
                                            StringBuilder file = new StringBuilder();

                                            foreach (char c in Program_Name_with_Arguments)
                                            {
                                                file.Append(c);
                                                fi = new System.IO.FileInfo(file.ToString());
                                                if (fi.Exists) break;
                                            }
                                            string Arguments = Program_Name_with_Arguments.Remove(0, file.Length);
                                            string Program_Name = fi.FullName;

                                            TASK_SendMessageWithMacAddress(ACK_RUN_COMMAND);
                                            Console.WriteLine(Program_Name + " " + Arguments);
                                            if (!(Commands.Contains(Program_Name_with_Arguments)))
                                            {
                                                Commands.Add(Arguments);
                                                RunCommand(Program_Name, Arguments);
                                            }
                                            accept = false;
                                        }
                                        Task.Factory.StartNew(() => WaitForACK(FINISH_RUN_COMMAND));
                                    }
                                    catch
                                    {
                                        TASK_SendMessageWithMacAddress(ERROR_MESSAGE);
                                    }
                                }
                                break;
                            }
                        case ACK_FINISH_RUN_COMMAND:
                            {
                                string MacAddresses = Get_MacAddressFromPaket(piece);
                                List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    accept = true;
                                }
                                break;
                            }
                        case FINISH_MESSAGE:
                            {
                                string MacAddresses = Get_MacAddressFromPaket(piece);
                                List<String> ClientsMacs = new List<string>(MacAddresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    Console.WriteLine("FINISH_FLAG");
                                    TASK_SendMessageWithMacAddress(ACK_FINISH);
                                }
                                else
                                {
                                    ConnectToAnotherAddress = false;
                                    connected = false;
                                }
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    if(ex.Message.Contains("An existing connection was forcibly closed by the remote host"))
                    {
                        TASK_Thread.Abort();
                    }
                }
            }
            TASK_ReceivingSocket.Close();
        }

        static void CreateConnection()
        {
            if (!(creating))
            {
                creating = true;
                try
                {
                    DestroyConnection();
                }
                catch
                {
                }
                ReceivingSocket = new UdpClient(Port);

                ReceivingIEP = new IPEndPoint(IPAddress.Any, ReceivingPort);

                IPAddress multicastaddress = IPAddress.Parse(MulticastAddress);

                IPEndPoint ep = new IPEndPoint(multicastaddress, Port);

                ReceivingSocket.JoinMulticastGroup(multicastaddress);
                ReceivingSocket.MulticastLoopback = true;
                Thread.Sleep(100);
                ReceivingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);
                creating = false;
            }
        }

        static void TASK_DestroyConnection()
        {
            try
            {
                TASK_ReceivingSocket.Close();
            }
            catch { }
        }

        static void DestroyConnection()
        {
            ReceivingSocket.Close();
        }

        static void Main(string[] args)
        {
            creating = false;
            ConnectToAnotherAddress = false;
            MulticastAddress = "225.100.0.1";
            Port = 9050;
            ReceivingPort = 0;
            CreateConnection();
            SetComputerDetails();

            UpdateStatus();            
        }
    }
}
