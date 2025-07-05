using c969_scheduler_program.Utils;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        // Address details
        public int AddressId { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        // City and Country details
        public string City { get; set; }
        public int CityId { get; set; }
        public string Country { get; set; }
        public int CountryId { get; set; }
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
                    cu.addressId,

                    a.address,
                    a.address2,
                    a.postalCode,
                    a.phone,
                    a.cityId,

                    ci.city,
                    ci.countryId,

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
                            Country = reader.GetString("country"),
                            AddressId = reader.GetInt32("addressId"),
                            CityId = reader.GetInt32("cityId"),
                            CountryId = reader.GetInt32("countryId"),
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

        public static Customer GetCustomerById(int customerId)
        {
            Customer customer = null;

            try
            {
                DBUtils.OpenConnection();

                string query = @"
            SELECT 
                cu.customerId,
                cu.customerName,
                cu.active,
                cu.createDate,
                cu.createdBy,
                cu.lastUpdate,
                cu.lastUpdateBy,

                a.addressId,
                a.address,
                a.address2,
                a.postalCode,
                a.phone,

                ci.cityId,
                ci.city,

                co.countryId,
                co.country

            FROM Customer cu
            JOIN Address a ON cu.addressId = a.addressId
            JOIN City ci ON a.cityId = ci.cityId
            JOIN Country co ON ci.countryId = co.countryId
            WHERE cu.customerId = @customerId";

                using (var cmd = new MySqlCommand(query, DBUtils.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@customerId", customerId);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customer = new Customer
                            {
                                CustomerId = reader.GetInt32("customerId"),
                                CustomerName = reader.GetString("customerName"),
                                IsActive = reader.GetBoolean("active"),
                                CreateDate = reader.GetDateTime("createDate"),
                                CreatedBy = reader.GetString("createdBy"),
                                LastUpdate = reader.GetDateTime("lastUpdate"),
                                LastUpdateBy = reader.GetString("lastUpdateBy"),

                                AddressId = reader.GetInt32("addressId"),
                                Address = reader.GetString("address"),
                                Address2 = reader.GetString("address2"),
                                PostalCode = reader.GetString("postalCode"),
                                Phone = reader.GetString("phone"),

                                CityId = reader.GetInt32("cityId"),
                                City = reader.GetString("city"),

                                CountryId = reader.GetInt32("countryId"),
                                Country = reader.GetString("country")
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error getting customer: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return customer;
        }

        public static bool DeleteCustomer(int customerId)
        {
            try
            {
                DBUtils.OpenConnection();

                // Step 1: Get the addressId linked to the customer
                int addressId = -1;
                using (var getCmd = new MySqlCommand("SELECT addressId FROM Customer WHERE customerId = @customerId", DBUtils.GetConnection()))
                {
                    getCmd.Parameters.AddWithValue("@customerId", customerId);
                    object result = getCmd.ExecuteScalar();
                    if (result == null || !int.TryParse(result.ToString(), out addressId))
                    {
                        return false;
                    }
                }

                // Step 2: Delete the address; the customer will be deleted automatically due to ON DELETE CASCADE
                using (var delCmd = new MySqlCommand("DELETE FROM Address WHERE addressId = @addressId", DBUtils.GetConnection()))
                {
                    delCmd.Parameters.AddWithValue("@addressId", addressId);
                    int rowsAffected = delCmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting customer/address: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }

        public static bool UpdateCustomer(Customer customer)
        {
            try
            {
                DBUtils.OpenConnection();
                var conn = DBUtils.GetConnection();

                using (var transaction = conn.BeginTransaction())
                {
                    // Update Customer
                    using (var cmd = new MySqlCommand(@"
                UPDATE Customer
                SET customerName = @customerName,
                    active = @isActive,
                    lastUpdate = NOW(),
                    lastUpdateBy = @lastUpdateBy
                WHERE customerId = @customerId;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customer.CustomerId);
                        cmd.Parameters.AddWithValue("@customerName", customer.CustomerName);
                        cmd.Parameters.AddWithValue("@isActive", customer.IsActive);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Address
                    using (var cmd = new MySqlCommand(@"
                    UPDATE Address
                    SET address = @address,
                        address2 = @address2,
                        postalCode = @postalCode,
                        phone = @phone,
                        lastUpdate = NOW(),
                        lastUpdateBy = @lastUpdateBy
                WHERE addressId = @addressId;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@addressId", customer.AddressId);
                        cmd.Parameters.AddWithValue("@address", customer.Address);
                        cmd.Parameters.AddWithValue("@address2", customer.Address2);
                        cmd.Parameters.AddWithValue("@postalCode", customer.PostalCode);
                        cmd.Parameters.AddWithValue("@phone", customer.Phone);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    // Update City
                    using (var cmd = new MySqlCommand(@"
                UPDATE City
                SET city = @city,
                    lastUpdate = NOW(),
                    lastUpdateBy = @lastUpdateBy
                WHERE cityId = @cityId;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@cityId", customer.CityId);
                        cmd.Parameters.AddWithValue("@city", customer.City);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    // Update Country
                    using (var cmd = new MySqlCommand(@"
                UPDATE Country
                SET country = @country,
                    lastUpdate = NOW(),
                    lastUpdateBy = @lastUpdateBy
                WHERE countryId = @countryId;", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@countryId", customer.CountryId);
                        cmd.Parameters.AddWithValue("@country", customer.Country);
                        cmd.Parameters.AddWithValue("@lastUpdateBy", CurrentUser.UserName);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating customer: " + ex.Message);
                return false;
            }
            finally
            {
                DBUtils.CloseConnection();
            }
        }


        public static bool AddCustomer(Customer customer)
        {
            int countryId = InsertCountry(customer.Country, CurrentUser.UserName);
            if (countryId == -1)
            {
                return false;
            }
            int cityId = InsertCity(customer.City, countryId, CurrentUser.UserName);
            if (cityId == -1)
            {
                return false;
            }
            int addressId = InsertAddress(
                customer.Address,
                customer.Address2,
                cityId,
                customer.PostalCode,
                customer.Phone,
                CurrentUser.UserName
            );
            if (addressId == -1)
            {
                return false;
            }
            int customerId = InsertCustomer(customer.CustomerName, addressId, CurrentUser.UserName, customer.IsActive);
            if (customerId == -1)
            {
                return false;
            }
            return true;
        }

        public static int InsertCountry(string countryName, string createdBy)
        {
            // The ID of the newly inserted country will be returned
            int newCountryId = -1;

            try
            {
                DBUtils.OpenConnection(); // Open the DB connection

                // SQL query to insert a new country and return its auto-incremented ID
                string insertCountry = @"
                    INSERT INTO Country (country, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (@country, NOW(), @createdBy, NOW(), @lastUpdateBy);
                    SELECT LAST_INSERT_ID();"; // Fetches the last inserted ID (for auto-increment)

                using (var cmd = new MySqlCommand(insertCountry, DBUtils.GetConnection()))//mySql command -- execution method cmd
                {
                    // Binds the country name to the above placeholders
                    cmd.Parameters.AddWithValue("@country", countryName);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                    // ExecuteScalar is used because we're expecting a single return value (the ID)
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newCountryId = id; // Assign the returned ID to newCountryId
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting country: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(); // Ensure the connection is closed no matter what
            }

            return newCountryId; // Return the ID of the new country, or -1 if it failed
        }

        public static int InsertCity(string cityName, int countryId, string createdBy)
        {
            int newCityId = -1;

            try
            {
                DBUtils.OpenConnection();

                string insertCity = @"
                    INSERT INTO City (city, countryId, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (@city, @countryId, NOW(), @createdBy, NOW(), @lastUpdateBy);
                    SELECT LAST_INSERT_ID();";

                using (var cmd = new MySqlCommand(insertCity, DBUtils.GetConnection()))
                {
                    // Bind parameters to placeholders
                    cmd.Parameters.AddWithValue("@city", cityName);
                    cmd.Parameters.AddWithValue("@countryId", countryId);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newCityId = id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting city: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection();
            }

            return newCityId;
        }

        public static int InsertAddress(string addressName, string address2, int cityId, string postalCode, string phone, string createdBy)
        {
            int newAddressId = -1;

            try
            {
                DBUtils.OpenConnection(); // Open DB connection

                // SQL query to insert a new address and return its auto-incremented ID
                string insertAddress = @"
                    INSERT INTO Address (address, address2, cityId, postalCode, phone, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (@address, @address2, @cityId, @postalCode, @phone, NOW(), @createdBy, NOW(), @lastUpdateBy);
                    SELECT LAST_INSERT_ID();"; // Fetches last inserted auto-increment ID

                using (var cmd = new MySqlCommand(insertAddress, DBUtils.GetConnection()))
                {
                    // Bind parameters to prevent SQL injection
                    cmd.Parameters.AddWithValue("@address", addressName);
                    cmd.Parameters.AddWithValue("@address2", address2);
                    cmd.Parameters.AddWithValue("@cityId", cityId);
                    cmd.Parameters.AddWithValue("@postalCode", postalCode);
                    cmd.Parameters.AddWithValue("@phone", phone);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                    // Expecting a single return value (the new addressId)
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newAddressId = id;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting address: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(); // Always close the DB connection
            }

            return newAddressId;
        }

        public static int InsertCustomer(string customerName, int addressId, string createdBy, bool isActive)
        {

            // The ID of the newly inserted customer will be returned
            int newCustomerId = -1;

            try
            {
                DBUtils.OpenConnection(); // Open the DB connection

                // SQL query to insert a new customer and return its auto-incremented ID
                string insertCustomer = @"
                    INSERT INTO Customer (customerName, addressId, active, createDate, createdBy, lastUpdate, lastUpdateBy)
                    VALUES (@customerName, @addressId, @isActive, NOW(), @createdBy, NOW(), @lastUpdateBy);
                    SELECT LAST_INSERT_ID();"; // Fetches the last inserted ID (for auto-increment)

                using (var cmd = new MySqlCommand(insertCustomer, DBUtils.GetConnection()))//mySql command -- execution method cmd
                {
                    // Binds the customer name to the above placeholders
                    cmd.Parameters.AddWithValue("@customerName", customerName);
                    cmd.Parameters.AddWithValue("@addressId", addressId);
                    cmd.Parameters.AddWithValue("@isActive", isActive);
                    cmd.Parameters.AddWithValue("@createdBy", createdBy);
                    cmd.Parameters.AddWithValue("@lastUpdateBy", createdBy);

                    // ExecuteScalar is used because we're expecting a single return value (the ID)
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int id))
                    {
                        newCustomerId = id; // Assign the returned ID to newCustomerId
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inserting customer: " + ex.Message);
            }
            finally
            {
                DBUtils.CloseConnection(); // Ensure the connection is closed no matter what
            }

            return newCustomerId; // Return the ID of the new customer, or -1 if it failed
        }


    }
}
