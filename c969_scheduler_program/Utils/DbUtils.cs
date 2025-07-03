using MySql.Data.MySqlClient;
using System;

namespace c969_scheduler_program.Utils
{
    public static class DBUtils
    {
        private static readonly string connectionString = "server=localhost;user=root;password=Djbrdm@1357laddiedog;database=c969_scheduler;";
        private static readonly MySqlConnection conn = new MySqlConnection(connectionString);

        public static MySqlConnection GetConnection()
        {
            return conn;
        }

        public static void OpenConnection()
        {
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
        }

        public static void CloseConnection()
        {
            if (conn.State != System.Data.ConnectionState.Closed)
                conn.Close();
        }
    }
}