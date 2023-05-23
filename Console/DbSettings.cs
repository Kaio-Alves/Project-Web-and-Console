using System.Data.SqlClient;

namespace ProjectCMD
{
    internal class DbSettings
    {
        protected static SqlConnection ConnectionDB()
        {
            SqlConnection connectionDb = new SqlConnection(@"Data Source=<IP DE CONEXÃO DO BANCO>; integrated security=SSPI;initial catalog=db_GasStation");
            connectionDb.Open();
            return connectionDb;
        }
    }
}