using c969_scheduler_program.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969_scheduler_program.Models
{
    internal class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        // Address details
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        // City and Country details
        public string City { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastUpdate { get; set; }
        public string LastUpdateBy { get; set; }


        public static List<Customer> GetAllCustomers()
        {
            var customers = new List<Customer>();

            string query = @"
                SELECT 
                    cu.customerId,
                    cu.customerName,
                    cu.active,
                    cu.createDate,
                    cu.createdBy,
                    cu.lastUpdate,
                    cu.lastUpdateBy,

                    a.address,
                    a.address2,
                    a.postalCode,
                    a.phone,

                    ci.city,
                    co.country

                FROM Customer cu
                JOIN Address a ON cu.addressId = a.addressId
                JOIN City ci ON a.cityId = ci.cityId
                JOIN Country co ON ci.countryId = co.countryId;
            ";

            try
            {
                DBUtils.OpenConnection();

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer
                        {
                            CustomerId = reader.GetInt32("customerId"),
                            CustomerName = reader.GetString("customerName"),
                            IsActive = reader.GetBoolean("active"),
                            CreateDate = reader.GetDateTime("createDate"),
                            CreatedBy = reader.GetString("createdBy"),
                            LastUpdate = reader.GetDateTime("lastUpdate"),

                            // Nullable string columns: check for DBNull first
                            LastUpdateBy = reader.IsDBNull(reader.GetOrdinal("lastUpdateBy")) ? "" : reader.GetString("lastUpdateBy"),

                            Address = reader.GetString("address"),

                            Address2 = reader.IsDBNull(reader.GetOrdinal("address2")) ? "" : reader.GetString("address2"),

                            PostalCode = reader.GetString("postalCode"),
                            Phone = reader.GetString("phone"),

                            City = reader.GetString("city"),
                            Country = reader.GetString("country")
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading customers: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return customers;
        }

        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                DBUtils.OpenConnection();

                string query = "DELETE FROM customer WHERE customerId = @customerId";
                using (MySqlCommand cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }
    }
}
