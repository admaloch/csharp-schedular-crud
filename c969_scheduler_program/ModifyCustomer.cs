using c969_scheduler_program.Models;
using c969_scheduler_program.Validators;
using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class ModifyCustomer : Form
    {
        private Customer currCustomer;
        public ModifyCustomer(Customer customer)
        {
            InitializeComponent();
            currCustomer = customer;
            this.Load += ModifyCustomer_Load;
        }
        private void ModifyCustomer_Load(object sender, EventArgs e)
        {
            SetInitialInputValues();
            InitializeInputEvents();
            CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);
        }
        private void SetInitialInputValues()
        {
            nameTxt.Text = currCustomer.CustomerName;
            addressTxt.Text = currCustomer.Address;
            address2Txt.Text = currCustomer.Address2;
            cityTxt.Text = currCustomer.City;
            countryTxt.Text = currCustomer.Country;
            zipTxt.Text = currCustomer.PostalCode;
            phoneTxt.Text = currCustomer.Phone;
            activeCheckBox.Checked = currCustomer.IsActive;
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
        private void submitBtn_Click(object sender, EventArgs e)
        {
            var (isValidated, formErrors) = CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);
            if (!isValidated)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Customer updatedCustomer = new Customer
            {
                CustomerId = currCustomer.CustomerId,
                AddressId = currCustomer.AddressId,
                CityId = currCustomer.CityId,
                CountryId = currCustomer.CountryId,
                CustomerName = nameTxt.Text.Trim(),
                Address = addressTxt.Text.Trim(),
                Address2 = address2Txt.Text.Trim(),
                City = cityTxt.Text.Trim(),
                Country = countryTxt.Text.Trim(),
                PostalCode = zipTxt.Text.Trim(),
                Phone = phoneTxt.Text.Trim(),
                IsActive = activeCheckBox.Checked
            };

            bool isSuccess = Customer.UpdateCustomer(updatedCustomer);
            if (!isSuccess)
            {
                MessageBox.Show("Failed to update item in DB", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Customer successfully modified!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close(); // Optional: close form or go back

        }
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
