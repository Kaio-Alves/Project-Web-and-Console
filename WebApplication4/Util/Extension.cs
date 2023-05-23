using System;
using System.Linq;

namespace ShoppingCart.Util
{
    public static class Extension
    {

        public static string GetHex(this byte[] content)
        {
            return BitConverter.ToString(content).Replace("-", "").ToLower();
        }

        public static byte[] HexToBin(this string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
