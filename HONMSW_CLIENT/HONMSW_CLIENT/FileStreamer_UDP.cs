using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class FileStreamer_UDP
    {

        FileStream fs;
        string path;
        List<Int64> received_pieces_checker;
        List<Int64> required_pieces_OLD;
        public List<Int64> received_pieces; // List of numbers

        public FileStreamer_UDP(string file_to_write)
        {            
            path = file_to_write;
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
            received_pieces = new List<Int64>();
            fs = File.OpenWrite(path);
            received_pieces_checker = new List<long>();
            required_pieces_OLD = new List<Int64>();
        }

        public void Close()
        {
            fs.Close();
        }

        public List<Int64> CheckMissingPieces(Int64 last_fileposition)
        {
            received_pieces.Sort();
            if (received_pieces.Count != 0)
            {
                Int64 last_received = last_fileposition;

                if (last_received == received_pieces.Count - 1)
                {
                    return null;
                }
                for (Int64 i = received_pieces_checker.Count; i <= last_received; i++)
                {
                    if (!(received_pieces_checker.Contains(i)))
                    {
                        received_pieces_checker.Add(i);
                    }
                }
                List<Int64> required_pieces = new List<Int64>();
                required_pieces = received_pieces_checker.Except(received_pieces).ToList();
               
                return required_pieces;
            }
            return null;
        }

        public void WriteSpecificPiece(FilePiece_UDP piece)
        {

            if (piece.number < 0)
            {
                // posiela file info
                return;
            }

            if (received_pieces.Contains(piece.number))
            {
                //Console.WriteLine("Detected Duplicate packet");
                //Console.WriteLine(piece.number);
                //received_pieces.Remove(piece.number);
            }
            else
            {
                received_pieces.Add(piece.number);
                //Console.WriteLine(piece.number + " " + piece.data.Length);
                fs.Seek(piece.number * FilePiece_UDP.data_size, SeekOrigin.Begin);
                fs.Write(piece.get_data(), 0, piece.data.Length);
            }
        }
    }
}
