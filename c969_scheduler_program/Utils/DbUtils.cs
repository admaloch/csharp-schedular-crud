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

        public static (bool isSuccess, string message) LoginUser(string username, string password)
        {
            try
            {
                OpenConnection();

                // Step 1: Check if username exists
                string checkUserQuery = "SELECT * FROM user WHERE userName = @username";
                using (MySqlCommand cmd = new MySqlCommand(checkUserQuery, GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            return (false, "Username not found.");
                        }
                    }
                }

                // Step 2: Check if password matches and account is active
                string checkPassQuery = "SELECT * FROM user WHERE userName = @username AND password = @password AND active = 1";
                using (MySqlCommand cmd = new MySqlCommand(checkPassQuery, GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return (true, "Login successful. Navigating to dashboard.");
                        }
                        else
                        {
                            return (false, "Incorrect password or inactive account.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return (false, "Login error: " + ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
