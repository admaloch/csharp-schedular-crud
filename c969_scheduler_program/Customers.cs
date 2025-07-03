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
        private void LoadCustomerData()
        {
            List<Customer> customers = Customer.GetAllCustomers();
            customerDGV.DataSource = customers;
            customerDGV.Columns["CreateDate"].Visible = false;
            customerDGV.Columns["CreatedBy"].Visible = false;
        }

        private void ConfigureCustomerDGV()
        {
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
                bool success = Customer.DeleteCustomer(customerId);
                if (success)
                {
                    MessageBox.Show("Customer deleted successfully.");
                    LoadCustomerData(); // Refresh the DataGridView
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            AddCustomer frm = new AddCustomer();
            frm.Show();
        }

        private void modifyBtn_Click(object sender, EventArgs e)
        {
            ModifyCustomer frm = new ModifyCustomer();
            frm.Show();
        }
    }
}
