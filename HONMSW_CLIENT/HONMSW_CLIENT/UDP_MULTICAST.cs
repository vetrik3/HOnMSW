using Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HONMSW_CLIENT
{
    class UDP_MULTICAST
    {
        public UdpClient ReceivingSocket;
        public IPEndPoint ReceivingIEP;
        public string MacAddress;
        string DestinationFilePath;
        public FileStreamer_UDP fileStreamer;
        bool end_receiving;
        bool File_receiving_OVER;
        Int64 last_number_of_position;
        bool over;
        bool error;

        bool resending;

        List<Int64> resend_packets;

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

        bool RunWithTimeout(ThreadStart threadStart, TimeSpan timeout)
        {
            Thread workerThread = new Thread(threadStart);

            workerThread.Start();

            bool finished = workerThread.Join(timeout);
            if (!finished)
                workerThread.Abort();

            return finished;
        }

        string Get_MacAddressFromPaket(FilePiece_UDP paket)
        {
            string clientMacAdress = Encoding.ASCII.GetString(paket.data, 0, paket.data.Length);
            return clientMacAdress;
        }

        string FileInfo(FilePiece_UDP piece)
        {
            string filename = Encoding.ASCII.GetString(piece.data, 0, piece.data.Length);
            Console.WriteLine("Receiving File Info: " + filename);
            return filename;
        }

        void SendAcceptByMacAddress(short state)
        {
            byte[] MacAdd_Data = Encoding.ASCII.GetBytes(MacAddress);
            FilePiece_UDP resend_piece = new FilePiece_UDP(state, info_position, MacAdd_Data);
            byte[] bytes = resend_piece.get_packet();
            ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
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

        void RESEND()
        {
            if (last_number_of_position != 0)
            {
                if (resend_packets != null)
                {
                    resending = false;
                    try
                    {
                        Console.WriteLine("NEED RESEND");
                        if(resend_packets.Count != 0)
                        {
                            byte[] MacAdd_Data = Encoding.ASCII.GetBytes(MacAddress);
                            FilePiece_UDP resend_piece = new FilePiece_UDP(SPEED_SLOW, -1, MacAdd_Data);
                            byte[] bytes = resend_piece.get_packet();
                            ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
                            Thread.Sleep(100);
                        }
                        while (resend_packets.Count != 0)
                        {
                            resending = true;                           
                            byte[] byteArray = new byte[resend_packets.Count * 8];
                            int pos = 0;
                            foreach (long number in resend_packets)
                            {
                                byte[] byteNumber = System.BitConverter.GetBytes(number);
                                Console.WriteLine(number);
                                System.Buffer.BlockCopy(byteNumber, 0, byteArray, pos * 8, byteNumber.Length);
                                pos++;
                            }
                            FilePiece_UDP resend_piece = new FilePiece_UDP(RESEND_MESSAGE, -1, byteArray);
                            byte[] bytes = resend_piece.get_packet();
                            ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
                            Thread.Sleep(1000);
                        }
                    }
                    catch
                    {

                    }
                    if(resending)
                    {
                        SendMacResendOver();
                        resending = false;
                    }
                }
            }           
        }

        void SendMacResendOver()
        {
            try
            {
                byte[] MacAdd_Data = Encoding.ASCII.GetBytes(MacAddress);
                FilePiece_UDP resend_piece = new FilePiece_UDP(RESENDING_OVER_MESSAGE, info_position, MacAdd_Data);
                byte[] bytes = resend_piece.get_packet();
                ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
            }
            catch { }
        }

        void ResendFileInfo()
        {
            try
            {
                FilePiece_UDP resend_piece = new FilePiece_UDP(INFO_FILE_MESSAGE, -1, new byte[0]);
                byte[] bytes = resend_piece.get_packet();
                ReceivingSocket.Send(bytes, bytes.Length, ReceivingIEP);
            }
            catch { }
        }

        void Receive_Over()
        {
            try
            {
                byte[] paket = ReceivingSocket.Receive(ref ReceivingIEP);
                FilePiece_UDP piece = FilePiece_UDP.parse_packet(paket);
                switch (piece.state)
                {
                    case SENDING_OVER:
                        {
                            string MacAdresses = Get_MacAddressFromPaket(piece);

                            List<String> ClientsMacs = new List<string>(MacAdresses.Split(','));

                            if (ClientsMacs.Contains(MacAddress))
                            {
                                SendAcceptByMacAddress(ACK_SENDING_OVER);
                            }
                            else
                            {
                                end_receiving = true;
                            }
                            break;
                        }
                }
            }
            catch { }
        }

        void Receving_OVER()
        {
            if (!(RunWithTimeout(Receive_Over, TimeSpan.FromMilliseconds(WAITING_TIME))))
            {
                Console.WriteLine("Bez spojenia");
                Console.ReadKey();
                ReceivingSocket.Close();
                Environment.Exit(0);
            }
        }

        void Receiving()
        {
            end_receiving = false;
            over = false;
            resending = false;
            while (!(end_receiving))
            {
                try
                {
                    byte[] paket = ReceivingSocket.Receive(ref ReceivingIEP);
                    FilePiece_UDP piece = FilePiece_UDP.parse_packet(paket);
                    switch (piece.state)
                    {
                        case DATA_SEND_MESSAGE:
                            {
                                if (fileStreamer != null)
                                {
                                    fileStreamer.WriteSpecificPiece(piece);
                                    resend_packets = fileStreamer.CheckMissingPieces(piece.number);
                                    last_number_of_position = piece.number;
                                    if (resend_packets != null && resend_packets.Count >= 10 && !(resending))
                                    {
                                        Thread resend = new Thread(new ThreadStart(RESEND));
                                        resend.Start();
                                        Thread.Sleep(300);
                                    }
                                }
                                else
                                {
                                    ResendFileInfo();
                                }
                                break;
                            }
                        case ERROR_MESSAGE:
                            {
                                ReceivingSocket.Close();
                                end_receiving = true;
                                error = true;
                                fileStreamer.Close();
                                break;
                            }
                        case UDP_MULTICAST_MESSAGE:
                            {
                                string MacAdresses = Get_MacAddressFromPaket(piece);

                                List<String> ClientsMacs = new List<string>(MacAdresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    SendAcceptByMacAddress(ACK_GET_READY_MESSAGE);
                                }


                                break;
                            }
                        case GET_READY_MESSAGE:
                            {
                                string MacAdresses = Get_MacAddressFromPaket(piece);

                                List<String> ClientsMacs = new List<string>(MacAdresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    SendAcceptByMacAddress(ACK_GET_READY_MESSAGE);
                                }


                                break;
                            }
                        case INFO_FILE_MESSAGE:
                            {
                                if (fileStreamer == null)
                                {
                                    DestinationFilePath = FileInfo(piece);
                                    VytvorPriecinok(DestinationFilePath);
                                    fileStreamer = new FileStreamer_UDP(DestinationFilePath);
                                    File_receiving_OVER = false;
                                    last_number_of_position = 0;
                                    SendAcceptByMacAddress(ACK_INFO_FILE_MESSAGE);
                                }
                                break;
                            }                        
                        case RESEND_MESSAGE:
                            {
                                if (fileStreamer != null)
                                {
                                    if (last_number_of_position != 0)
                                    {
                                        fileStreamer.WriteSpecificPiece(piece);
                                        resend_packets = fileStreamer.CheckMissingPieces(piece.number);
                                    }
                                }
                                break;
                            }
                        case FILE_SENDING_OVER:
                            {
                                try
                                {
                                    if (fileStreamer != null)
                                    {
                                        resend_packets = fileStreamer.CheckMissingPieces(piece.number);

                                        if (!(resending))
                                        {
                                            Thread resend = new Thread(new ThreadStart(RESEND));
                                            resend.Start();
                                            Thread.Sleep(300);
                                            if (!(resending))
                                            {
                                                File_receiving_OVER = true;
                                                fileStreamer.Close();
                                                fileStreamer = null;
                                                SendAcceptByMacAddress(ACK_FILE_SENDING_OVER);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SendAcceptByMacAddress(ACK_FILE_SENDING_OVER);
                                    }
                                }
                                catch
                                {
                                    SendAcceptByMacAddress(ACK_FILE_SENDING_OVER);
                                }
                                break;
                            }
                        case RESENDING_OVER_MESSAGE:
                            {
                                string MacAdresses = Get_MacAddressFromPaket(piece);

                                List<String> ClientsMacs = new List<string>(MacAdresses.Split(','));

                                if (ClientsMacs.Contains(MacAddress))
                                {
                                    if (fileStreamer != null)
                                    {
                                        resend_packets = fileStreamer.CheckMissingPieces(last_number_of_position);
                                        if (!(resending))
                                        {
                                            Thread resend = new Thread(new ThreadStart(RESEND));
                                            resend.Start();
                                            Thread.Sleep(300);
                                            if (!(resending))
                                            {
                                                File_receiving_OVER = true;
                                                fileStreamer.Close();
                                                fileStreamer = null;
                                                SendAcceptByMacAddress(RESENDING_OVER_MESSAGE);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        SendAcceptByMacAddress(RESENDING_OVER_MESSAGE);
                                    }
                                }
                                break;
                            }
                        case SENDING_OVER:
                            {
                                if (!(File_receiving_OVER))
                                {
                                    resend_packets = fileStreamer.CheckMissingPieces(piece.number);
                                    if (!(resending))
                                    {
                                        Thread resend = new Thread(new ThreadStart(RESEND));
                                        resend.Start();
                                        Thread.Sleep(300);
                                        if (!(resending))
                                        {
                                            File_receiving_OVER = true;
                                            fileStreamer.Close();
                                            fileStreamer = null;
                                            SendAcceptByMacAddress(ACK_SENDING_OVER);
                                        }
                                    }
                                }
                                if ((File_receiving_OVER))
                                {
                                    Receving_OVER();
                                }
                                break;
                            }
                    }
                }
                catch
                {
                    resending = false;
                    Console.WriteLine("Something wrong with UDP MULTICAST Server");
                    fileStreamer.Close();
                    ReceivingSocket.Close(); 
                    end_receiving = true;
                    error = true;
                }
            }
        }

        public bool UDP_MULTICAST_RUN()
        {
            try
            {
                error = false;
                Receiving();
                Console.WriteLine("Uspesne Dokoncenie Posielania");
                return error;
            }
            catch
            {
                return true;
            }
        }
    }
}
