using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class FilePiece
    {
        /* 2 bytes: state
         * 8 bytes: piece number (Int64)
         * 
         * rest: data
         */

        public const int data_size = 1024;
        public const int header_size = 10;

        public byte[] data;
        /* 
        * Status
        * 0: sending file info
        * 1: sending data
        * 2: sending end of file
        * 3: sending end of copying
        * -2: resending file info
        * -1: resending pakets
         * -3: slow down
        */
        public Int16 state;
        public Int64 number;

        public FilePiece(Int16 st, Int64 num, byte[] file_data)
        {
            data = file_data;
            number = num;
            state = st;
        }

        public int get_packet_size()
        {
            return header_size + data.Length;
        }

        public byte[] get_data()
        {
            return data;
        }
 

        public byte[] get_packet()
        {
            byte[] status = System.BitConverter.GetBytes(state);
            byte[] piece_number = System.BitConverter.GetBytes(number);

            byte[] b = new byte[header_size + data.Length]; //  + data_size

            status.CopyTo(b, 0);
            piece_number.CopyTo(b, 2);
            data.CopyTo(b, header_size);

            return b;
        }

        public static byte[] get_missing_packet(Int64 missing_number)
        {
            byte[] number_data = System.BitConverter.GetBytes(missing_number);
            FilePiece piece = new FilePiece(-1, -1, number_data);

            return piece.get_packet();
        }

        static public FilePiece parse_packet(byte[] packet)
        {
            if (packet.Length < header_size)
            {
                return null;
            }
            Int32 data_length = packet.Length - 10;
            if (data_length < 0)
            {
            }

            Int16 status = System.BitConverter.ToInt16(packet, 0);
            Int64 piece_number = System.BitConverter.ToInt64(packet, 2);

            byte[] data = new byte[data_length];

            Buffer.BlockCopy(packet, 10, data, 0, data_length);

            FilePiece piece = new FilePiece(status, piece_number, data);

            return piece;
        }
    }
}
