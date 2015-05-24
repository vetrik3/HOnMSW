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
    class TCP_UNICAST
    {
        private const int BufferSize = 1024;
        public string IP_Address;
        public int PORT;
        bool over;
        int position;
        public List<string> TempFiles;
        public string destinationDir;
        public string srcDirName;
        string SendingFile;
        FileStreamer file_stream;
        IPAddress ipAd;
        public TcpListener Listener;
        public Socket socket;
        bool error_over;

        public long WAITING_TIME;

        bool RunWithTimeout(ThreadStart threadStart, TimeSpan timeout)
        {
            Thread workerThread = new Thread(threadStart);

            workerThread.Start();

            bool finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }

        string DestinationFileName()
        {
            if (srcDirName != "UPDATE")
            {
                String[] Splitter = null;
                String destFileName = "";
                String DestPath = "";

                SendingFile = TempFiles[position];

                Splitter = SendingFile.Split('\\');

                destFileName = Splitter[Splitter.Length - 1];

                Splitter = SendingFile.Replace(srcDirName, "").Split('\\');

                DestPath = destinationDir + "\\";

                for (int i = 0; i < Splitter.Length - 1; i++)
                {
                    DestPath += Splitter[i] + "\\";
                }

                destFileName = System.IO.Path.Combine(DestPath, destFileName);

                return destFileName;
            }
            else
            {
                SendingFile = TempFiles[position];
                return TempFiles[position];
            }
        }

        void readyForSending()
        {
            try
            {
                if (position == TempFiles.Count)
                {
                    over = true;
                }
                ipAd = IPAddress.Parse(IP_Address);
                Listener = new TcpListener(IPAddress.Any, PORT);
                Listener.Start();
                socket = Listener.AcceptSocket();
                byte[] data = new byte[100];
                int k = socket.Receive(data);
                byte[] data_Upgrade = new byte[k];
                Buffer.BlockCopy(data, 0, data_Upgrade, 0, k);
                string string_Data = System.Text.Encoding.UTF8.GetString(data_Upgrade);
                if (string_Data == "Ready")
                {
                    ASCIIEncoding AsciiE = new ASCIIEncoding();
                    if (over)
                    {
                        socket.Send(AsciiE.GetBytes("OVER"));
                    }
                    else
                    {
                        string destFileName = DestinationFileName();
                        position++;                        
                        socket.Send(AsciiE.GetBytes(destFileName));                        
                    }
                    socket.Close();
                    Listener.Stop();
                }
                else
                {
                    Console.WriteLine("Something is wrong --> Client is not ready.");
                }
            }
            catch (Exception e)
            {
                socket.Close();
                Listener.Stop();
                error_over = true;
                Console.WriteLine("Error..... " + e.StackTrace);
            }
        }

        void SendTCP()
        {
            try
            {
                TcpClient client = null;
                NetworkStream netstream = null;

                client = new TcpClient(IP_Address, PORT);
                netstream = client.GetStream();
                file_stream = new FileStreamer(SendingFile);
                
                while (true)
                {
                    FilePiece fp = file_stream.GetNextPiece();
                    if (fp != null)
                    {
                        byte[] bytes = fp.data;
                        if (bytes != null)
                        {
                            netstream.Write(bytes, 0, (int)bytes.Length);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                netstream.Close();
                client.Close();
            }
            catch
            {
                error_over = true;
            }
        }

        public string TCP_UNICAST_RUN()
        {
            position = 0;
            PORT = 20000;
            error_over = false;
            over = false;
            while (!over)
            {
                if ((RunWithTimeout(readyForSending, TimeSpan.FromMilliseconds(WAITING_TIME))))
                {
                    SendTCP();
                }
                else
                {
                    error_over = true;
                    over = true;
                }
            }    
            if(error_over)
            {
                return "ERROR";
            }
            else
            {
                return "OK";
            }        
        }
    }
}
