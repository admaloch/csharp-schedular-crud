using c969_scheduler_program.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program.Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string Url { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }


        public static bool InsertAppointment(Appointment appt)
        {
            try
            {
                DBUtils.OpenConnection();

                string query = @"
            INSERT INTO Appointment (
                customerId, userId, title, description, location, contact, type, url,
                start, end, createDate, createdBy, lastUpdate, lastUpdateBy
            ) VALUES (
                @customerId, @userId, @title, @description, @location, @contact, @type, @url,
                @start, @end, NOW(), @createdBy, NOW(), @lastUpdateBy
            );
        ";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@customerId", appt.CustomerId);
                    cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId);
                    cmd.Parameters.AddWithValue("@title", appt.Title);
                    cmd.Parameters.AddWithValue("@description", appt.Description);
                    cmd.Parameters.AddWithValue("@location", appt.Location);
                    cmd.Parameters.AddWithValue("@contact", appt.Contact);
                    cmd.Parameters.AddWithValue("@type", appt.Type);
                    cmd.Parameters.AddWithValue("@url", appt.Url ?? "");
                    cmd.Parameters.AddWithValue("@start", appt.Start);
                    cmd.Parameters.AddWithValue("@end", appt.End);
                    cmd.Parameters.AddWithValue("@createdBy", CurrentUser.UserName);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName); // Assuming same user
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting appointment: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }

        public static bool UpdateAppointment(Appointment appt)
        {
            try
            {
                DBUtils.OpenConnection();


                string query = @"
            UPDATE Appointment
            SET
                customerId = @customerId,
                userId = @userId,
                title = @title,
                description = @description,
                location = @location,
                contact = @contact,
                type = @type,
                url = @url,
                start = @start,
                end = @end,
                lastUpdate = NOW(),
                lastUpdateBy = @lastUpdateBy
            WHERE appointmentId = @appointmentId";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appt.AppointmentId);
                    cmd.Parameters.AddWithValue("@customerId", appt.CustomerId);
                    cmd.Parameters.AddWithValue("@userId", CurrentUser.UserId); // or appt.UserId if stored
                    cmd.Parameters.AddWithValue("@title", appt.Title);
                    cmd.Parameters.AddWithValue("@description", appt.Description);
                    cmd.Parameters.AddWithValue("@location", appt.Location);
                    cmd.Parameters.AddWithValue("@contact", appt.Contact);
                    cmd.Parameters.AddWithValue("@type", appt.Type);
                    cmd.Parameters.AddWithValue("@url", appt.Url);
                    cmd.Parameters.AddWithValue("@start", appt.Start);
                    cmd.Parameters.AddWithValue("@end", appt.End);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating appointment: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }

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
                                Title = reader.GetString("title"),
                                Type = reader.GetString("type"),
                                Description = reader.GetString("description"),
                                Location = reader.GetString("location"),
                                Contact = reader.GetString("contact"),
                                Url = reader.GetString("url"),
                                Start = reader.GetDateTime("start"),
                                End = reader.GetDateTime("end"),
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

        public static Appointment GetAppointmentById(int appointmentId)
        {
            Appointment appointment = null;

            try
            {
                DBUtils.OpenConnection();

                string query = @"
            SELECT 
                ap.*, 
                c.customerName
            FROM Appointment ap
            JOIN Customer c ON ap.customerId = c.customerId
            WHERE ap.appointmentId = @appointmentId";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            appointment = new Appointment
                            {
                                AppointmentId = reader.GetInt32("appointmentId"),
                                CustomerId = reader.GetInt32("customerId"),
                                CustomerName = reader.GetString("customerName"),
                                Title = reader.GetString("title"),
                                Type = reader.GetString("type"),
                                Description = reader.GetString("description"),
                                Location = reader.GetString("location"),
                                Contact = reader.GetString("contact"),
                                Url = reader.GetString("url"),
                                Start = reader.GetDateTime("start"),
                                End = reader.GetDateTime("end")
                                // You can add CreateDate, CreatedBy, LastUpdate, LastUpdateBy if needed
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving appointment: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return appointment;
        }

        public static bool DeleteAppointment(int appointmentId)
        {
            try
            {
                DBUtils.OpenConnection();

                string query = "DELETE FROM appointment WHERE appointmentId = @appointmentId";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@appointmentId", appointmentId);
                    int affectedRows = cmd.ExecuteNonQuery();

                    return affectedRows > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting appointment: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }



    }
}
