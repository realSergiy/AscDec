using System;
using System.Security.Cryptography;

namespace AscDec
{
    public class RandomSecure
    {
        readonly RNGCryptoServiceProvider _rnd;

        public RandomSecure()
        {
            _rnd = new RNGCryptoServiceProvider();
        }

        public double GetDouble()
        {
            var bytes = GetBytes(8);
            var ul = BitConverter.ToUInt64(bytes, 0) / (1 << 11);
            var d = ul / (double)(1UL << 53);
            return d;
        }

        byte[] GetBytes(int bytesNumber)
        {
            byte[] buffer = new byte[bytesNumber];
            _rnd.GetBytes(buffer);
            return buffer;
        }
    }
}