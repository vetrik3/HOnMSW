using Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HONMSW_CLIENT
{
    class TCP_UNICAST
    {
        private const int BufferSize = 1024;
        string SaveFileName;
        public UdpClient ReceivingSocket;
        public IPEndPoint ReceivingIEP;
        public string ServerIPAddress;
        public int ServerPORT;
        bool over;
        bool error;
        TcpListener Listener;
        TcpClient tcpClient;
        Stream socket;

        public long WAITING_TIME;

        const int INFO_MESSAGE = -1;
        const int ACK_TCP_UNICAST = 14;

        void SendMessage(short state, Int64 position, byte[] data)
        {
            try
            {
                FilePiece resend_piece = new FilePiece(state, position, data);
                byte[] bytes = resend_piece.get_packet();
                ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
                error = true;
            }
        }

        void SendMessageWithIPAddress(short state)
        {
            string hostName = Dns.GetHostName();
            string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();
            byte[] Sending_Info_Data = Encoding.ASCII.GetBytes(myIP);
            SendMessage(ACK_TCP_UNICAST, INFO_MESSAGE, Sending_Info_Data);
        }

        void VytvorPriecinok(string path)
        {
            path = path.Replace(Path.GetFileName(path), "");
            if (path.Substring(0, 1) == @"\")
            {
                path = path.Substring(1, path.Length - 1);
            }
            if (!(Directory.Exists(path)))
            {
                Directory.CreateDirectory(path);
            }
        }

        bool RunWithTimeout(ThreadStart threadStart, TimeSpan timeout)
        {
            Thread workerThread = new Thread(threadStart);

            workerThread.Start();

            bool finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }

        void getReadyLooper()
        {
            long WAITING_TIME_FOR_LOOP = 10000;
            if ((RunWithTimeout(getReady, TimeSpan.FromMilliseconds(WAITING_TIME_FOR_LOOP))))
            {
            }
            else
            {
                getReadyLooper();
            }
        }

        void getReady()
        {
            try
            {
                String str = "Ready";
                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(str);

                socket.Write(ba, 0, ba.Length);

                byte[] data = new byte[1024];
                int k = socket.Read(data, 0, 1024);
                byte[] data_Upgrade = new byte[k];
                Buffer.BlockCopy(data, 0, data_Upgrade, 0, k);
                string string_Data = System.Text.Encoding.UTF8.GetString(data_Upgrade);

                Console.WriteLine(string_Data);
                if (string_Data == "OVER")
                {
                    over = true;
                }
                else
                {
                    SaveFileName = string_Data;
                    VytvorPriecinok(SaveFileName);
                }
                socket.Close();
                tcpClient.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        void receive()
        {
            byte[] RecData = new byte[BufferSize];
            int RecBytes;
            Listener = null;
            try
            {
                Listener = new TcpListener(IPAddress.Any, ServerPORT);
                Listener.Start();
            }
            catch (Exception ex)
            {
                Listener.Stop();
                Console.WriteLine(ex.Message);
                receive();
            }
            TcpClient client = null;
            NetworkStream netstream = null;
            FileStream Fs = null;
            try
            {
                client = Listener.AcceptTcpClient();
                netstream = client.GetStream();
                if (SaveFileName != string.Empty)
                {
                    Fs = new FileStream(SaveFileName, FileMode.OpenOrCreate, FileAccess.Write);
                    while ((RecBytes = netstream.Read(RecData, 0, RecData.Length)) > 0)
                    {
                        Fs.Write(RecData, 0, RecBytes);
                    }
                    Fs.Close();
                }

                netstream.Close();
                client.Close();
                Listener.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                netstream.Close();
                client.Close();
                Listener.Stop();
                Fs.Close();
                over = true;
            }
        }

        void WaitForServer()
        {            
            try
            {
                tcpClient = new TcpClient();
                tcpClient.Connect(ServerIPAddress, ServerPORT);
                socket = tcpClient.GetStream();
                Thread.Sleep(100);
            }
            catch
            {
                try
                {
                    Console.WriteLine("Waiting For Server: " + ServerIPAddress);
                    SendMessageWithIPAddress(ACK_TCP_UNICAST);
                    if (!error)
                    {
                        WaitForServer();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void TCP_UNICAST_RUN()
        {
            over = false;
            error = false;
            ServerPORT = 20000;
            while (!over && !error)
            {
                if (!(RunWithTimeout(WaitForServer, TimeSpan.FromMilliseconds(WAITING_TIME))))
                {
                    Console.WriteLine("Something is wrong --> Server is OFF");
                    over = true;
                    error = true;
                }
                else
                {
                    if ((RunWithTimeout(getReadyLooper, TimeSpan.FromMilliseconds(WAITING_TIME))))
                    {
                        receive();
                    }
                    else
                    {
                        over = true;
                    }
                }
            }
        }
    }
}
