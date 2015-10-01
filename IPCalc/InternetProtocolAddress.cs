using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPCalc
{
    class InternetProtocolAddress
    {
        public UInt32 IPAddress = 0;

        const UInt32 byteone = 4278190080;
        const UInt32 bytetwo = 16711680;
        const UInt32 bytethree = 65280;
        const UInt32 bytefour = 255;

        byte count = 0;

        static Func<byte> nextPointer = null;

        public InternetProtocolAddress(UInt32 ip)
        {
            IPAddress = ip;
        }

        public InternetProtocolAddress(byte firstoctet,byte secondoctet,byte thirdoctet,byte fourthoctet)
        {
            IPAddress +=(UInt32) (firstoctet * Math.Pow(256, 3));
            IPAddress += (UInt32)(secondoctet * Math.Pow(256, 2));
            IPAddress += (UInt32)(thirdoctet * 256);
            IPAddress += (UInt32)(fourthoctet);
        }

        public byte getNextByte()
        {
            
            if (count > 3)
                count = 0;
            if (count == 0)
                nextPointer = firstbyte;
            else if (count == 1)
                nextPointer= secondbyte;
            else if (count == 2)
                nextPointer=thirdbyte;
            else
                nextPointer=fourthbyte;
            ++count;
            return nextPointer();
        }

        public byte firstbyte()
        {
            return(byte)((IPAddress & byteone)>>24);
        }

        public byte secondbyte()
        {
            return (byte)((IPAddress & bytetwo) >> 16);
        }

        public byte thirdbyte()
        {
            return (byte)((IPAddress & bytethree) >> 8);
        }

        public byte fourthbyte()
        {
            return (byte)((IPAddress & bytefour));
        }

        public override string ToString()
        {
            return firstbyte().ToString() + "." + secondbyte().ToString() + "." + thirdbyte().ToString() + "." + fourthbyte().ToString();
        }

        public string ToBinaryString()
        {
            return Binary.byte2String(firstbyte()) + "." + Binary.byte2String(secondbyte()) + "." + Binary.byte2String(thirdbyte()) + "." + Binary.byte2String(fourthbyte());
        }

    }
}
