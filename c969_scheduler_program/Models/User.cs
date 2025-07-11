

using c969_scheduler_program.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace c969_scheduler_program.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public bool Active { get; set; }

        // You can also keep your static CurrentUser fields
        public static int CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }

        public static User GetUserById(int userId)
        {
            User user = null;

            try
            {
                DBUtils.OpenConnection();

                string query = "SELECT * FROM User WHERE userId = @userId";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                UserId = reader.GetInt32("userId"),
                                UserName = reader.GetString("userName"),
                                Active = reader.GetInt32("active") == 1
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving user: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return user;
        }
    }

}
