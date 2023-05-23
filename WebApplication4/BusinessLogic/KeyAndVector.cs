using ShoppingCart.Util;

namespace WebCrypto.BusinessLogic
{
    public class KeyAndVector
    {

        internal readonly byte[] Key;
        internal readonly byte[] Vector;

        public KeyAndVector(byte[] key, byte[] vector) 
        {
            Key = key;
            Vector = vector;
        }

        public KeyAndVector(string keyHex, string vectorHex)
        {
            Key = keyHex.HexToBin();
            Vector = vectorHex.HexToBin();
        }

        public string GetHexKey()
        {
            return Key.GetHex();
        }

        public string GetHexVector()
        {
            return Vector.GetHex();
        }

    }
}
