using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class Customers : Form
    {
        public Customers()
        {
            InitializeComponent();
            LoadCustomerData();
            ConfigureCustomerDGV();
        }
        public void LoadCustomerData()
        {
            List<Customer> customers = Customer.GetAllCustomers();
            customerDGV.DataSource = customers;
        }



        private void ConfigureCustomerDGV()
        {
            customerDGV.Columns["CreateDate"].Visible = false;
            customerDGV.Columns["CreatedBy"].Visible = false;
            customerDGV.Columns["CountryId"].Visible = false;
            customerDGV.Columns["CityId"].Visible = false;
            customerDGV.Columns["AddressId"].Visible = false;

            customerDGV.ReadOnly = true;
            customerDGV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            customerDGV.RowHeadersVisible = false;
            customerDGV.MultiSelect = false;
            customerDGV.AllowUserToAddRows = false;
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsRowSelected(customerDGV))
            {
                MessageBox.Show("Please select a customer to delete.");
                return;
            }

            int customerId = Utilities.GrabDgvRowId(customerDGV);

            var confirm = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm", MessageBoxButtons.YesNo);
            if (confirm == DialogResult.Yes)
            {
                Customer currCustomer = Customer.GetCustomerById(customerId);
                if (currCustomer == null)
                {
                    return;
                }
                bool success = Customer.DeleteCustomer(customerId, currCustomer.AddressId);
                if (success)
                {
                    MessageBox.Show("Customer deleted successfully.");
                    LoadCustomerData(); // Refresh the DataGridView
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var addForm = new AddCustomer();
            var result = addForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadCustomerData(); // Refresh after a successful add
            }
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsRowSelected(customerDGV))
            {
                MessageBox.Show("Please select a customer to modify.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int customerId = Convert.ToInt32(customerDGV.CurrentRow.Cells["CustomerId"].Value);
            Customer currCustomer = Customer.GetCustomerById(customerId);
            if (currCustomer == null)
            {
                MessageBox.Show("Error: Unable to access customer data.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var modifyForm = new ModifyCustomer(currCustomer);
            var result = modifyForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadCustomerData();
            }
        }

        private void exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
