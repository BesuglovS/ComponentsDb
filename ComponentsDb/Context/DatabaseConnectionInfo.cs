namespace ComponentsDb.Context
{
    public class DatabaseConnectionInfo
    {
        public DatabaseConnectionInfo()
        {
        }

        public DatabaseConnectionInfo(string connectionServer, string connectionDatabaseName,
            string connectionUsername, string connectionPassword)
        {
            ConnectionServer = connectionServer;
            ConnectionDatabaseName = connectionDatabaseName;
            ConnectionUsername = connectionUsername;
            ConnectionPassword = connectionPassword;
        }

        public string ConnectionServer;
        public string ConnectionDatabaseName;
        public string ConnectionUsername;
        public string ConnectionPassword;
    }
}
