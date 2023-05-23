using System.Data.SqlClient;

namespace ProjectCMD
{
    internal class InsertDb:DbSettings
    {
        public static void Insert(string car, int time)
        {
            SqlCommand comandDb = new SqlCommand();
            SqlDataReader readerDb;
            comandDb.Connection = DbSettings.ConnectionDB();
            comandDb.CommandText = $"INSERT INTO Cars (Car,Time) VALUES ('{car}',{time})";
            readerDb = comandDb.ExecuteReader();
        }
    }
}
