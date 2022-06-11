using System.Data.SqlClient;

namespace GameLibrary.Server
{
    public class AppDb : IDisposable
    {
        public SqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new SqlConnection(connectionString);
        }

        public void Dispose() => Connection.Dispose();
    }
}
