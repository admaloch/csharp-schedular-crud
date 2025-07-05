using c969_scheduler_program.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace c969_scheduler_program.Models
{
    internal class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } // Optional for display
        public int UserId { get; set; }

        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Url { get; set; }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }


        public static List<Appointment> GetAppointmentsForUserByDate(int userId, DateTime date)
        {
            var appointments = new List<Appointment>();

            try
            {
                DBUtils.OpenConnection();

                string query = @"
            SELECT 
                a.appointmentId,
                a.customerId,
                c.customerName,
                a.userId,
                a.title,
                a.type,
                a.description,
                a.location,
                a.contact,
                a.url,
                a.start,
                a.end,
                a.createDate,
                a.createdBy,
                a.lastUpdate,
                a.lastUpdateBy
            FROM Appointment a
            JOIN Customer c ON a.customerId = c.customerId
            WHERE a.userId = @userId
              AND DATE(a.start) = @selectedDate
            ORDER BY a.start;
        ";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@selectedDate", date.Date); // Ensures only date is matched

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            appointments.Add(new Appointment
                            {
                                AppointmentId = reader.GetInt32("appointmentId"),
                                CustomerId = reader.GetInt32("customerId"),
                                CustomerName = reader.GetString("customerName"),
                                UserId = reader.GetInt32("userId"),
                                Title = reader.GetString("title"),
                                Type = reader.GetString("type"),
                                Description = reader.GetString("description"),
                                Location = reader.GetString("location"),
                                Contact = reader.GetString("contact"),
                                Url = reader.GetString("url"),
                                Start = reader.GetDateTime("start"),
                                End = reader.GetDateTime("end"),
                                CreateDate = reader.GetDateTime("createDate"),
                                CreatedBy = reader.GetString("createdBy"),
                                LastUpdate = reader.GetDateTime("lastUpdate"),
                                LastUpdateBy = reader.GetString("lastUpdateBy")
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading appointments: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return appointments;
        }



    }
}
