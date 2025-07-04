using c969_scheduler_program.Validators;
using c969_scheduler_program.Models;

using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class AddCustomer : Form
    {
        public AddCustomer()
        {
            InitializeComponent();
            this.Load += (s, e) => InitializeInputEvents();
            CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);

        }
        private void InitializeInputEvents()
        {
            nameTxt.TextChanged += SharedInputChanged;
            addressTxt.TextChanged += SharedInputChanged;
            address2Txt.TextChanged += SharedInputChanged;
            cityTxt.TextChanged += SharedInputChanged;
            countryTxt.TextChanged += SharedInputChanged;
            zipTxt.TextChanged += SharedInputChanged;
            phoneTxt.TextChanged += SharedInputChanged;
        }
        private void SharedInputChanged(object sender, EventArgs e)//connect inputs into shared listener
        {
            CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var (isValidated, formErrors) = CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);
            if (!isValidated)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer newCustomer = new Customer
            {
                CustomerName = nameTxt.Text.Trim(),
                Address = addressTxt.Text.Trim(),
                Address2 = address2Txt.Text.Trim(),
                City = cityTxt.Text.Trim(),
                Country = countryTxt.Text.Trim(),
                PostalCode = zipTxt.Text.Trim(),
                Phone = phoneTxt.Text.Trim(),
                IsActive = activeCheckBox.Checked
            };

            bool isSuccess = Customer.AddCustomer(newCustomer);
            if (!isSuccess)
            {
                MessageBox.Show("Failed to add item to DB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Customer successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close(); // Optional: close form or go back
        }

    }
}
