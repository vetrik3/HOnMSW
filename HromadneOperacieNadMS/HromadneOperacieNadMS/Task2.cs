using SERVER_MULTICAST;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    class Task2
    {
        UdpClient SendingSocket;
        IPEndPoint SendingIEP;
        IPEndPoint ReceivingIEP;
        UDP_MULTICAST UDP_SERVER;
        TCP_UNICAST TCP_SERVER;
        public TaskOptions Global_TaskOptions;

        public List<String> WaitingClientsMacs = new List<string>();
        public List<String> WaitingClientsMacs_ForFinishCMD = new List<string>();

        public bool STOPPED;

        bool TCP_ACCEPT;
        string TCP_IP_ADDRESS;
        
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

        const int SPEED_SLOW = -23;

        const int FILE_SENDING_OVER = -22;
        const int ACK_FILE_SENDING_OVER = -21;

        const int OVER_MESSAGE = -20;
        const int ACK_OVER_MESSAGE = -19;

        const int INFO_FILE_MESSAGE = 20;
        const int ACK_INFO_FILE_MESSAGE = 21;

        const int DATA_SEND_MESSAGE = 1;

        const int RESEND_MESSAGE = 22;
        const int RESENDING_OVER_MESSAGE = 23;

        const int GET_READY_MESSAGE = 24;
        const int ACK_GET_READY_MESSAGE = 25;

        const int SENDING_OVER = 26;
        const int ACK_SENDING_OVER = 27;
        

        public class TaskOptions
        {
            public string Name { get; set; }
            public int NumberOFTask { get; set; }
            public bool WakeOnLan { get; set; }
            public List<String> Commands { get; set; }
            public List<String> MacAddresses { get; set; }
            public bool UDP_multicast { get; set; }
            public bool TCP_unicast { get; set; }
            public string TargetComputers { get; set; }
            public List<String> TargetComputers_List { get; set; }
            public bool TurnOff { get; set; }
            public string TargetDirectory { get; set; }
            public List<String> TempFiles { get; set; }
            public List<String> taskLines { get; set; }
            public int DetailsLine { get; set; }
            public List<String> IPAddresses { get; set; }
            public string SourceDirectory { get; set; }
            public int WAITING_TIME { get; set; }
            public bool update { get; set; }

            public TaskOptions(string filename)
            {
                Name = filename;
                MacAddresses = new List<string>();
                Commands = new List<string>();
                TempFiles = new List<string>();
                taskLines = new List<string>();
                IPAddresses = new List<string>();
                TargetComputers_List = new List<string>();
            }

            public void AddToMacAddresses(string computerName)
            {
                string computerFileName = @".\Computers\" + computerName + ".my";
                if (File.Exists(computerFileName))
                {
                    try
                    {
                        List<string> line = File.ReadAllLines(computerFileName).ToList();
                        if (line.Count != 0)
                        {
                            MacAddresses.Add(line[1].Split(new string[] { "||" }, StringSplitOptions.None)[1]);
                            TargetComputers_List.Add(computerName);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Problem pri zasahovani do suboru");
                        AddToMacAddresses(computerName);
                    }
                }
            }

            public void RemoveMacAddress(string MacAddress)
            {
                int index = MacAddresses.FindIndex(x => x.StartsWith(MacAddress));
                AddStep(MacAddress, "DISCONNECTED", DateTime.Now.ToString(), "1");
                TargetComputers_List.RemoveAt(index);
                MacAddresses.Remove(MacAddress);       
            }

            public void EditDetailsLine(string status, string finishDate, string DoneComputers)
            {
                string line = taskLines[DetailsLine];
                string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                taskLines[DetailsLine] = "DETAILS||" + status + "||" + splitter[2] + "||" + finishDate + "||" + splitter[4] + "||" + DoneComputers;
                SaveToFile();
            }

            public void AddStep(string MacAddress, string stepInfo, string Date, string status)
            {
                int index = MacAddresses.FindIndex(x => x.StartsWith(MacAddress));
                if (index == -1)
                {
                    taskLines.Add("STEP||" + MacAddress + "||" + stepInfo + "||" + Date + "||" + status);
                }
                else
                {
                    taskLines.Add("STEP||" + TargetComputers_List[index] + "||" + stepInfo + "||" + Date + "||" + status);
                }
                SaveToFile();
            }

            public void SaveToFile()
            {
                File.WriteAllLines(@".\TaskDetails\" + Name + "." + NumberOFTask, taskLines);
            }
        }

        private void UDP_IncomingData(IAsyncResult ar)
        {
            try
            {
                byte[] bResp = SendingSocket.EndReceive(ar, ref ReceivingIEP);

                FilePiece_UDP piece = FilePiece_UDP.parse_packet(bResp);
                switch (piece.state)
                {
                    case ACK_RUN_COMMAND:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            Global_TaskOptions.AddStep(MacAddress, "RUN CMD", DateTime.Now.ToString(), "0");
                            WaitingClientsMacs.Remove(MacAddress);
                            break;
                        }
                    case FINISH_RUN_COMMAND:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            Global_TaskOptions.AddStep(MacAddress, "FINISH RUN CMD", DateTime.Now.ToString(), "0");
                            WaitingClientsMacs_ForFinishCMD.Remove(MacAddress);

                            byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(MacAddress);
                            SendMessage(ACK_FINISH_RUN_COMMAND, INFO_MESSAGE, Sending_Info_Data);
                            break;
                        }
                    case ACK_FINISH:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            Global_TaskOptions.AddStep(MacAddress, "FINISH", DateTime.Now.ToString(), "0");
                            WaitingClientsMacs.Remove(MacAddress);
                            break;
                        }
                    case ACK_FLAG:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            WaitingClientsMacs.Remove(MacAddress);
                            break;
                        }
                    case ACK_TCP_UNICAST:
                        {
                            TCP_IP_ADDRESS = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            TCP_ACCEPT = true;
                            break;
                        }
                    case ACK_TURN_OFF:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            Global_TaskOptions.AddStep(MacAddress, "TURN OFF", DateTime.Now.ToString(), "0");
                            WaitingClientsMacs.Remove(MacAddress);
                            break;
                        }
                    case ACK_CONNECT_MESSAGE:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            Console.WriteLine("ACK CONNECT: " + MacAddress);
                            WaitingClientsMacs.Remove(MacAddress);
                            break;
                        }
                    case RESEND_MESSAGE:
                        {
                            UDP_SERVER.resend = true;
                            UDP_SERVER.recending_piece = piece;
                            break;
                        }
                    case SPEED_SLOW:
                        {
                            UDP_SERVER.resend = true;
                            UDP_SERVER.sending_time_speed += 2;
                            if (UDP_SERVER.WaitingClients_RESEND == null)
                            {
                                UDP_SERVER.WaitingClients_RESEND = new List<string>();
                            }
                            string MacAddress = Get_MacAddressFromPaket(piece);
                            if (!(UDP_SERVER.WaitingClients_RESEND.Contains(MacAddress)))
                            {
                                if (MacAddress.Length > 17)
                                {
                                    MacAddress = MacAddress.Substring(0, 17);
                                }
                                UDP_SERVER.WaitingClients_RESEND.Add(MacAddress);
                            }

                            break;
                        }
                    case ACK_GET_READY_MESSAGE:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            UDP_SERVER.WaitingClients.Remove(MacAddress);
                            break;
                        }
                    case RESENDING_OVER_MESSAGE:
                        {
                            string MacAdd = Get_MacAddressFromPaket(piece);
                            UDP_SERVER.WaitingClients_RESEND.Remove(MacAdd);
                            if (UDP_SERVER.WaitingClients_RESEND.Count == 0)
                            {
                                UDP_SERVER.resend = false;
                            }
                            break;
                        }
                    case INFO_FILE_MESSAGE:
                        {
                            UDP_SERVER.sendFileInfo();
                            break;
                        }
                    case ACK_INFO_FILE_MESSAGE:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            UDP_SERVER.WaitingClients.Remove(MacAddress);
                            break;
                        }
                    case ACK_SENDING_OVER:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            UDP_SERVER.WaitingClients.Remove(MacAddress);
                            break;
                        }
                    case ACK_FILE_SENDING_OVER:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            UDP_SERVER.WaitingClients.Remove(MacAddress);
                            break;
                        }
                    case ACK_OVER_MESSAGE:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            UDP_SERVER.WaitingClients.Remove(MacAddress);
                            break;
                        }
                    case ERROR_MESSAGE:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            if (WaitingClientsMacs_ForFinishCMD != null)
                            {
                                if(WaitingClientsMacs_ForFinishCMD.Count != 0)
                                {
                                    WaitingClientsMacs_ForFinishCMD.Remove(MacAddress);
                                }
                            }
                            if(UDP_SERVER != null)
                            {
                                UDP_SERVER.clientsMACs.Remove(MacAddress); 
                                UDP_SERVER.WaitingClients.Remove(MacAddress);
                            }
                            WaitingClientsMacs.Remove(MacAddress);
                            Global_TaskOptions.RemoveMacAddress(MacAddress);
                            break;
                        }
                    default:
                        {
                            string MacAddress = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
                            break;
                        }
                }
                SendingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);
            }
            catch
            {
                Console.WriteLine("SOMETHING WRONG WITH SOCKET IN TASK");
            }
        }

        string Get_MacAddressFromPaket(FilePiece_UDP paket)
        {
            string clientMacAdress = Encoding.ASCII.GetString(paket.data, 0, paket.data.Length);
            return clientMacAdress;
        }

        private void LoadComputerMacAddresses()
        {
            if (Global_TaskOptions.TargetComputers.Contains(','))
            {
                string[] splitter = Global_TaskOptions.TargetComputers.Split(',');
                foreach (string computerName in splitter)
                {
                    Global_TaskOptions.AddToMacAddresses(computerName);
                }
            }
            else
            {
                Global_TaskOptions.AddToMacAddresses(Global_TaskOptions.TargetComputers);
            }
        }

        private void LoadTaskOptions(string fileName)
        {
            if (File.Exists(fileName))
            {             
                Global_TaskOptions.taskLines = File.ReadAllLines(fileName).ToList();
                bool commands = false;
                bool targetDirectory = false;
                foreach (string line in Global_TaskOptions.taskLines)
                {
                    if (line.Contains("WOL||"))
                    {
                        commands = false;
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.WakeOnLan = Convert.ToBoolean(splitter[1]);
                    }
                    if (commands)
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.Commands.Add(splitter[1]);
                    }
                    if (line.Contains("TargetComputers||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.TargetComputers = splitter[1];
                        LoadComputerMacAddresses();
                    }
                    if (line.Contains("Commands||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        if (splitter[1] != "")
                        {
                            Global_TaskOptions.Commands.Add(splitter[1]);
                        }
                        commands = true;
                    }
                    if (line.Contains("UDP||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        if (Convert.ToBoolean(splitter[1]))
                        {
                            Global_TaskOptions.UDP_multicast = true;
                        }
                        else
                        {
                            Global_TaskOptions.TCP_unicast = true;
                        }
                    }
                    if (line.Contains("TurnOff||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.TurnOff = Convert.ToBoolean(splitter[1]);
                    }
                    if (line != "")
                    {
                        if (targetDirectory)
                        {
                            Global_TaskOptions.TempFiles.Add(line);
                        }
                    }
                    if (line.Contains("WaitingTime||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.WAITING_TIME = Convert.ToInt16(splitter[1])*1000*60;
                    }
                    if (line.Contains("TargetDirectory||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.TargetDirectory = splitter[1];
                        targetDirectory = true;
                    }
                    if (line.Contains("SourceDirectory||"))
                    {
                        string[] splitter = line.Split(new string[] { "||" }, StringSplitOptions.None);
                        Global_TaskOptions.SourceDirectory = splitter[1];
                    }
                }
            }
            else
            {
                MessageBox.Show(fileName + " not exist");
            }
        }

        public void TCP_Unicast()
        {
            foreach (string MacAddress in Global_TaskOptions.MacAddresses)
            {
                TCP_ACCEPT = false;
                while (!(TCP_ACCEPT))
                {
                    string hostName = Dns.GetHostName();
                    string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
                    string Info_data = myIP;
                    Info_data += "#" + MacAddress;
                    byte[] Sending_Data = Encoding.ASCII.GetBytes(Info_data);
                    SendMessage(TCP_UNICAST_MESSAGE, INFO_MESSAGE, Sending_Data);
                    Thread.Sleep(1000);
                }
                TCP_SERVER = new TCP_UNICAST();
                TCP_SERVER.destinationDir = Global_TaskOptions.TargetDirectory;
                TCP_SERVER.srcDirName = Global_TaskOptions.SourceDirectory;
                TCP_SERVER.TempFiles = Global_TaskOptions.TempFiles;
                TCP_SERVER.IP_Address = TCP_IP_ADDRESS;
                TCP_SERVER.WAITING_TIME = Global_TaskOptions.WAITING_TIME;
                TCP_SERVER.PORT = Global_TaskOptions.NumberOFTask;
                if(TCP_SERVER.TCP_UNICAST_RUN() == "ERROR")
                {
                    Global_TaskOptions.RemoveMacAddress(MacAddress);
                }
                else
                {
                    Global_TaskOptions.AddStep(MacAddress, "TCP", DateTime.Now.ToString(), "0");
                }
            }
        }

        public void UDP_Multicast()
        {
            UDP_SERVER = new UDP_MULTICAST();
            UDP_SERVER.SourceDirectory = Global_TaskOptions.SourceDirectory;
            UDP_SERVER.TargetDirectory = Global_TaskOptions.TargetDirectory;
            UDP_SERVER.TempFiles = Global_TaskOptions.TempFiles;
            UDP_SERVER.SendingSocket = SendingSocket;
            UDP_SERVER.ReceivingIEP = ReceivingIEP;
            UDP_SERVER.SendingIEP = SendingIEP;
            UDP_SERVER.WAITING_TIME = Global_TaskOptions.WAITING_TIME;
            UDP_SERVER.clientsMACs = new List<string>(Global_TaskOptions.MacAddresses);
            UDP_SERVER.UDP_MULTICAST_RUN();            
        }

        public void SendMessage(short state, Int64 position, byte[] data)
        {
            try
            {
                FilePiece resend_piece = new FilePiece(state, position, data);
                byte[] bytes = resend_piece.get_packet();
                SendingSocket.Send(bytes, bytes.Length, SendingIEP);
            }
            catch
            {

            }
        }

        public void Create_Connections(string address)
        {
            try
            {
                IPAddress multicastaddress = IPAddress.Parse(address);
                ReceivingIEP = new IPEndPoint(IPAddress.Any, Global_TaskOptions.NumberOFTask);

                SendingIEP = new IPEndPoint(multicastaddress, 9000);

                SendingSocket = new UdpClient(ReceivingIEP);

                SendingSocket.BeginReceive(new AsyncCallback(UDP_IncomingData), ReceivingIEP);                
            }
            catch
            {
                Console.WriteLine("Cannoct create Connection");
            }
        }

        public void Destroy_Connections()
        {
            try
            {
                STOPPED = true;
                SendingSocket.Close();
                TCP_SERVER.Listener.Stop();
                TCP_SERVER.socket.Close();
            }
            catch
            {
                
            }
        }

        public void PrepareTask(string Name)
        {
            STOPPED = false;
            if (Directory.Exists(@".\TaskDetails\"))
            {
                string[] executedTasks = Directory.GetFiles(@".\TaskDetails\", "*.*");
                Global_TaskOptions = new TaskOptions(Name);
                Global_TaskOptions.NumberOFTask = 0;                
                foreach (string task in executedTasks)
                {
                    string fileName = Path.GetFileName(task);
                    int NumberOfFile = Convert.ToInt16(fileName.Split('.')[1]);
                    if (Global_TaskOptions.NumberOFTask < NumberOfFile)
                    {
                        Global_TaskOptions.NumberOFTask = NumberOfFile;
                    }
                }
                Global_TaskOptions.NumberOFTask++;
                LoadTaskOptions(@".\Tasks\" + Name);
                WaitingClientsMacs = new List<string>(Global_TaskOptions.MacAddresses);
                CreateTaskDetails(Name);   
            }
        }

        private void CreateTaskDetails(string taskName)
        {                       
            DateTime Date_Now = DateTime.Now;
            Global_TaskOptions.DetailsLine = Global_TaskOptions.taskLines.Count;
            Global_TaskOptions.taskLines.Add("DETAILS||3||" + Date_Now.ToString() + "||NONE||" + Global_TaskOptions.MacAddresses.Count + "||0");
            Global_TaskOptions.SaveToFile();            
        }

        public void Stop()
        {
            if (!STOPPED)
            {
                STOPPED = true;
                WaitingClientsMacs = new List<string>(Global_TaskOptions.MacAddresses);

                foreach (string MacAddress in WaitingClientsMacs)
                {
                    Global_TaskOptions.RemoveMacAddress(MacAddress);
                }
            }
        }
    }
}
