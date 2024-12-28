using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TurboporrOS
{
    public class Font
    {
        public readonly Byte[] data;
        public readonly UInt16 take;
        public readonly Byte bpl;
        public Font (Byte[] data)
        {
            this.data = data;

            switch (this.data[0])
            {
                case 0x00:
                    this.take = 8;
                    this.bpl = 0x01;
                    break;
                case 0x03:
                    this.take = 32;
                    this.bpl = 16;
                    break;
                case 0x0F:
                    this.take = 128;
                    this.bpl = 32;
                    break;
                case 0x3F:
                    this.take = 512;
                    this.bpl = 64;
                    break;
                default:
                    break;

            }
        }

        public List<Tuple<UInt16,UInt16>> getDataAt (UInt16 index)
        {
            StringBuilder sb = new StringBuilder();
            List <Tuple < UInt16, UInt16>> returnData = new List<Tuple<UInt16, UInt16>>();
            foreach (Byte b in this.getRawDataAt(index))
                sb.Append(b.toBinary());

            UInt16 x = 0, y = 0;
            Byte fixedBpl=(Byte)(this.bpl - 1);

            foreach (Char c in sb.ToString())
            {
                if (c == '1') returnData.Add(new Tuple<UInt16, UInt16>(x, y));

                if (x == fixedBpl)
                {
                    ++y;
                    x = 0;
                }
                else ++x;
            }

            return returnData;
        }

        public List<Byte> getRawDataAt (UInt16 index) { return this.data.skip(1+(this.take*index)).take(this.take); }


    }

    public static class LinqSubstitutes
    {
        public static List<Byte> skip(this Byte[] bytes, Int32 count)
        {
            List<Byte> data = new List<Byte>();
            Int32 ctr = count;

            while (true)
            {
                try { data.Add(bytes[ctr]); }
                catch { break; }

                ++ctr;

            }
            return data;
        }

        public static List<Byte> take (this List<Byte> bytes, Int32 count)
        {
            List<Byte> data = new List<Byte>();
            Int32 ctr = 0;

            while (ctr<=count)
            {
                data.Add(bytes[ctr]);
                ++ctr; 
            }

            return data;
        }

        public static String toBinary (this Byte b)
        {
            StringBuilder str = new StringBuilder(8);
            Int32[] vs= new Int32[8];

            Byte i = 0;
            while (i<vs.Length)
            {
                vs[vs.Length - 1 - i] = ((b & (1 << i)) != 0) ? 1 : 0;
                ++i;
            }

            foreach (Int32 num in vs) str.Append(num);

            return str.ToString();
        }
    }
}
