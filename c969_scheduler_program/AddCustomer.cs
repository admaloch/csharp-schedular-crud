using c969_scheduler_program.Validators;
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

        }
    }
}
