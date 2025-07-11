using c969_scheduler_program.Models;
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
        // Check credentials against DB, set CurrentUser on success
        public static (bool IsSuccess, string Message) TryLogin(string username, string password)
        {
            try
            {
                OpenConnection();

                string query = "SELECT userId, userName FROM user WHERE userName = @username AND password = @password AND active = 1";
                using (var cmd = new MySqlCommand(query, GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int userId = reader.GetInt32("userId");
                            string userName = reader.GetString("userName");
                            User.CurrentUserId = userId;
                            User.CurrentUserName = userName;

                            return (true, "Login successful\nInicio de sesión exitoso.");
                        }
                        else
                        {
                            return (false, "Incorrect username or password, or account inactive\nNombre de usuario o contraseña incorrectos, o cuenta inactiva.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                return (false, "Database connection error: " + ex.Message + "\nError de conexión con la base de datos.");
            }
            catch (Exception ex)
            {
                return (false, "Login error: " + ex.Message + "\nError de inicio de sesión.");
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}