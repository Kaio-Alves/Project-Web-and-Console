using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using WebCrypto.BusinessLogic;
using ShoppingCart.Util;

namespace ShoppingCart.BusinessLogic
{
    public abstract class DataBaseSettings
    {
        public static readonly DataBaseSettings DB = new DataBase();
        //Listas de metodos a serem implementados pelas sub-classes
        protected abstract IMongoDatabase ConnectionDataBaseClient();
        protected abstract IMongoDatabase ConnectionSecurityClient();
        public abstract string SetDataBase(string clientName, string productName, string infoProduct);
        public abstract KeyAndVector GetDataBaseSecurity(string clientName);

        //Lista de sub-classes
        internal class DataBase : DataBaseSettings
        {
            protected override IMongoDatabase ConnectionDataBaseClient()
            {
                //conexão mongoDB
                MongoClient dbClient = new MongoClient("<STRING DE CONEXÃO>");
                var iMongoDatabase = dbClient.GetDatabase("ViaHubClient");
                return iMongoDatabase;
            }
            protected override IMongoDatabase ConnectionSecurityClient()
            {
                //conexão mongoDB
                MongoClient dbClient = new MongoClient("<STRING DE CONEXÃO>");
                var iMongoDatabase = dbClient.GetDatabase("ViaHubSecurityClient");
                return iMongoDatabase;
            }
            public override KeyAndVector GetDataBaseSecurity(string clientName)
            {
                var collectionSecurity = ConnectionSecurityClient().GetCollection<BsonDocument>("KeyAndVector");
                var documentFilter = Builders<BsonDocument>.Filter.Eq("Client Name", clientName);
                var documentFind = collectionSecurity.Find(documentFilter).FirstOrDefault();

                if (documentFind == null)
                {
                    var createKey = CriptoServiceType.AES.GenerateKey().Key;
                    var createVector = CriptoServiceType.AES.GenerateKey().IV;
                    var key = createKey.GetHex();
                    var vector = createVector.GetHex();
                    var documentSecurity = new BsonDocument { { "Client Name", clientName }, { "Key", key },{ "Vector", vector },
                    { "DateTime", DateTime.Now } };
                    collectionSecurity.InsertOne(documentSecurity);
                    documentFind = collectionSecurity.Find(documentFilter).FirstOrDefault();
                }
                var keyValue = documentFind.GetElement("Key").Value.ToString().HexToBin();
                var vectorValue = documentFind.GetElement("Vector").Value.ToString().HexToBin();
                var keyAndVector = new KeyAndVector(keyValue, vectorValue);
                return keyAndVector;
            }
            public override string SetDataBase(string clientName, string productName, string infoProduct)
            {
                var collectionClient = ConnectionDataBaseClient().GetCollection<BsonDocument>(clientName);

                if (collectionClient == null)
                {
                    ConnectionDataBaseClient().CreateCollection(clientName, null, default);
                    collectionClient = ConnectionDataBaseClient().GetCollection<BsonDocument>(clientName);
                }

                var document = new BsonDocument { { "Product's Name", productName }, { "Info", infoProduct },
                    { "DateTime", DateTime.Now } };
                collectionClient.InsertOne(document);
                return "Ok";
            }
        }
    }
}
    



