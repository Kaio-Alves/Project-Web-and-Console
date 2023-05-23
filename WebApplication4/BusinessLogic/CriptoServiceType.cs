using ShoppingCart.BusinessLogic;
using System.Security.Cryptography;
using System.Text;
using ShoppingCart.Util;

namespace ShoppingCart.BusinessLogic
{
    public abstract class CriptoServiceType 
    {
        //Lista de tipos
        public static readonly CriptoServiceType AES = new AesType();
        //Listas de metodos a serem implementados pelas sub-classes
        public abstract string Encrypt(string productName, string infoProduct, string clientName);
        public abstract AesCryptoServiceProvider GenerateKey();
        //Lista de sub-classes
        internal  class AesType : CriptoServiceType
        {
            public override AesCryptoServiceProvider GenerateKey()
            {
                var crypt_provider = new AesCryptoServiceProvider();
                crypt_provider.BlockSize = 128;
                crypt_provider.KeySize = 256;
                crypt_provider.GenerateIV();
                crypt_provider.GenerateKey();
                return crypt_provider;
            }
            public override string Encrypt(string productName, string infoProduct, string clientName)
            {
                var crypt_provider = new AesCryptoServiceProvider();
                crypt_provider.Key = DataBaseSettings.DB.GetDataBaseSecurity(clientName).Key;
                crypt_provider.IV = DataBaseSettings.DB.GetDataBaseSecurity(clientName).Vector;
                ICryptoTransform transform = crypt_provider.CreateEncryptor();
                byte[] statusProductBytes = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(infoProduct), 0, infoProduct.Length);
                var createConnection = DataBaseSettings.DB.SetDataBase(clientName, productName, statusProductBytes.GetHex());
                return "Product successfully registered,save this information: " + "(Key): "
                    + crypt_provider.Key.GetHex() + " / (Vector): "+ crypt_provider.IV.GetHex();
            }
        }
    }
}

