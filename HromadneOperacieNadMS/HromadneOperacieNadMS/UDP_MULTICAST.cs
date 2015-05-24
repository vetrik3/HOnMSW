using SERVER_MULTICAST;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HromadneOperacieNadMS
{
    class UDP_MULTICAST
    {
        public List<String> TempFiles;
        public List<string> clientsMACs;
        public List<string> WaitingClients;
        public List<string> WaitingClients_RESEND;
        string FileNameDestination;
        string FileNameSource;
        public string TargetDirectory;
        public string SourceDirectory;

        bool error;

        const int EMPTY = 0;

        public int sending_time_speed;

        const int ERROR_MESSAGE = -4;
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

        const int UDP_MULTICAST_MESSAGE = 11;

        const int info_position = -1;

        public long WAITING_TIME;

        public bool resend;

        public UdpClient SendingSocket;
        public IPEndPoint SendingIEP;
        public IPEndPoint ReceivingIEP;
        FileStreamer_UDP file_stream;
        Int64 file_position;
        public FilePiece_UDP recending_piece;    

        void RESENDING()
        {
            try
            {
                while (WaitingClients_RESEND.Count != 0)
                {
                    FilePiece_UDP piece = recending_piece;
                    List<long> resend_packets = new List<long>();
                    for (int i = 0; i < piece.data.Length / 8; i++)
                    {
                        resend_packets.Add(System.BitConverter.ToInt64(piece.data, i * 8));
                    }
                    if (resend_packets.Count != 0)
                    {
                        foreach (long number in resend_packets)
                        {
                            byte[] data = file_stream.GetSpecificChunk(number);
                            if (data != null)
                            {
                                FilePiece_UDP piece_resend = new FilePiece_UDP(RESEND_MESSAGE, number, data);
                                byte[] bytes = piece_resend.get_packet();
                                if (sending_time_speed != 0)
                                {
                                    Thread.Sleep(sending_time_speed);
                                }
                                SendingSocket.Send(bytes, bytes.Length, SendingIEP);
                            }
                        }
                    }

                    var result = String.Join(",", WaitingClients_RESEND.ToArray());
                    byte[] MacAdd_Data = Encoding.ASCII.GetBytes(result);
                    SengMessage(RESENDING_OVER_MESSAGE, file_position, MacAdd_Data);
                    Thread.Sleep(500);
                }
            }
            catch
            {
            }
            if (WaitingClients_RESEND.Count == 0)
            {
                resend = false;
            }
        }

        private void SengMessage(short state, long pos, byte[] data)
        {
            try
            {

                FilePiece_UDP piece = new FilePiece_UDP(state, pos, data);
                byte[] bytes = piece.get_packet();
                SendingSocket.Send(bytes, bytes.Length, SendingIEP);
            }
            catch { }
        }
        public void sendFileInfo()
        {
            WaitingClients = new List<string>(clientsMACs);
            while (WaitingClients.Count != 0)
            {
                byte[] fileName = Encoding.ASCII.GetBytes(FileNameDestination);
                SengMessage(INFO_FILE_MESSAGE, info_position, fileName);
                var result = String.Join(",", WaitingClients.ToArray());
                byte[] MacAdd_Data = Encoding.ASCII.GetBytes(result);
                SengMessage(GET_READY_MESSAGE, info_position, MacAdd_Data);
                while (resend)
                {
                    RESENDING();
                    Thread.Sleep(300);
                }
                Thread.Sleep(300);
            }
        }

        void FILE_SEND_OVER()
        {            
            WaitingClients = new List<string>(clientsMACs);
            while (WaitingClients.Count != EMPTY)
            {
                var MacAddresses_withColumn = String.Join(",", WaitingClients.ToArray());
                byte[] MacAdd_Data = Encoding.ASCII.GetBytes(MacAddresses_withColumn);
                SengMessage(FILE_SENDING_OVER, file_position, MacAdd_Data);
                while (resend)
                {
                    RESENDING();
                    Thread.Sleep(300);
                }
                Thread.Sleep(300);
            }
        }

        void SENDING()
        {
            int speed_step = 0;
            file_stream = new FileStreamer_UDP(FileNameSource);
            if (file_stream != null)
            {
                while (true)
                {
                    FilePiece_UDP fp = file_stream.GetNextPiece();
                    if (fp != null)
                    {
                        byte[] bytes = fp.get_packet();
                        if (bytes != null)
                        {
                            speed_step += 1;
                            while (resend)
                            {
                                speed_step = 0;
                                RESENDING();
                                Thread.Sleep(300);
                            }
                            if (speed_step == 50)
                            {
                                speed_step = 0;
                                if (sending_time_speed != 0)
                                {
                                    sending_time_speed -= 2;
                                }
                            }
                            
                            SendingSocket.Send(bytes, bytes.Length, SendingIEP);                            
                            file_position = fp.position;
                            if (sending_time_speed != 0)
                            {
                                Thread.Sleep(sending_time_speed);
                            }
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
            }
            return;

        }

        void SEND(string sourcePath, string destinationPath)
        {
            FileNameSource = sourcePath;
            FileNameDestination = destinationPath;
            if (!(RunWithTimeout(sendFileInfo, TimeSpan.FromMilliseconds(WAITING_TIME))))
            {
                for (int position = 0; position < WaitingClients.Count; position++)
                {
                    clientsMACs.Remove(WaitingClients[position]);
                }
            }

            resend = false;
            if (clientsMACs.Count != 0)
            {              
                SENDING();                
                FILE_SEND_OVER();
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

        public static string Get_MacAddressFromPaket(FilePiece_UDP paket)
        {
            string clientMacAdress = Encoding.ASCII.GetString(paket.data, 0, paket.data.Length);
            return clientMacAdress;
        }

        void readyForSending()
        {
            try
            {
                WaitingClients = new List<string>(clientsMACs);

                while (WaitingClients.Count != 0)
                {
                    var result = String.Join(",", WaitingClients.ToArray());
                    byte[] MacAdd_Data = Encoding.ASCII.GetBytes(result);
                    SengMessage(UDP_MULTICAST_MESSAGE, info_position, MacAdd_Data);
                    Thread.Sleep(300);
                }
            }
            catch
            {
                error = true;
            }
        }      

        void SEND_OVER_EMPTY_MACS()
        {
            while (true)
            {
                SengMessage(SENDING_OVER, file_position, new byte[0]);
                Thread.Sleep(10);
            }
        }

        void SEND_OVER()
        {
            WaitingClients = new List<string>(clientsMACs);
            while (WaitingClients.Count != 0)
            {
                var result = String.Join(",", WaitingClients.ToArray());
                byte[] MacAdd_Data = Encoding.ASCII.GetBytes(result);
                SengMessage(SENDING_OVER, file_position, MacAdd_Data);
                Thread.Sleep(300);
            }            
        }

        public List<string> UDP_MULTICAST_RUN()
        {
            sending_time_speed = 0;
            error = false;

            if (!(RunWithTimeout(readyForSending, TimeSpan.FromMilliseconds(WAITING_TIME))))
            {
                for (int position = 0; position < WaitingClients.Count; position++)
                {
                    clientsMACs.Remove(WaitingClients[position]);
                }
            }
            if (!(error))
            {
                try
                {
                    foreach (String SuborCely in TempFiles.ToArray())
                    {
                        if (SourceDirectory != "UPDATE")
                        {
                            //Splitter je rozdelovac, ktory nam rozdeli cestu suboru podla '\'
                            String[] Splitter = null;
                            //SuborNaOdoslanie je cesta aj so suborom nakonci, kde sa ma subor odoslat
                            String CielSub = "";
                            //CestaOdosPriec je cesta bez subora na konci, kde sa ma subor odoslat
                            String CestaOdosPriec = "";

                            Splitter = SuborCely.Split('\\');

                            //Vymaze zo zdrojovej cesty vsetko a ostane len nazov suboru
                            CielSub = Splitter[Splitter.Length - 1];

                            Splitter = SuborCely.Replace(SourceDirectory, "").Split('\\');

                            CestaOdosPriec = TargetDirectory + "\\";

                            for (int i = 0; i < Splitter.Length - 1; i++)
                            {
                                CestaOdosPriec += Splitter[i] + "\\";
                            }
                            //vytvori celu cestu
                            CielSub = System.IO.Path.Combine(CestaOdosPriec, CielSub);

                            string SendingFilePath = SuborCely;
                            string DestinationFilePath = CielSub;

                            SEND(SendingFilePath, DestinationFilePath);
                        }
                        else
                        {
                            SEND(SuborCely, SuborCely);
                        }

                    }
                    RunWithTimeout(SEND_OVER, TimeSpan.FromMilliseconds(WAITING_TIME));
                    for (int position = 0; position < WaitingClients.Count; position++)
                    {
                        clientsMACs.Remove(WaitingClients[position]);
                    }
                    RunWithTimeout(SEND_OVER_EMPTY_MACS, TimeSpan.FromMilliseconds(100));
                }
                catch
                {
                    error = true;                   
                }
            }
            return clientsMACs;
        }

    }
}
