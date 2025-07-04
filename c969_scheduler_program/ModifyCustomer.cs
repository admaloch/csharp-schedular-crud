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
            LoadCustomerData();
            this.Load += (s, e) => InitializeInputEvents();
            CustomerValidator.ValidateCustomer(nameTxt, addressTxt, cityTxt, countryTxt, zipTxt, phoneTxt);
        }

        private void LoadCustomerData()
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

        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
