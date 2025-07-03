using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;

using MySql.Data.MySqlClient;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;



namespace c969_scheduler_program
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.Load += Login_Load;
        }
        public void Login_Load(object sender, EventArgs e) // add parameters here
        {
            usernameTxt.TextChanged += SharedInputChanged; // Fix: attach to TextChanged event (not += a method to Text)
            passwordTxt.TextChanged += SharedInputChanged;
            ValidateSharedInputs();
        }

        public void SharedInputChanged(object sender, EventArgs e)
        {
            ValidateSharedInputs();
        }

        public (bool isValid, string errorMessage) ValidateSharedInputs()
        {
            bool allValid = true;
            var errors = new List<string>();

            // Username: Required
            if (!ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(usernameTxt.Text), usernameTxt))
            {
                errors.Add("User Name is required.");
                allValid = false;
            }

            // Password: Required
            bool passwordNotEmpty = !string.IsNullOrWhiteSpace(passwordTxt.Text);
            if (!ValidationUtils.SetValidationState(passwordNotEmpty, passwordTxt))
            {
                errors.Add("Password is required.");
                allValid = false;
            }
            else
            {
                // Additional password rules
                string password = passwordTxt.Text;

                if (password.Length < 6)
                {
                    errors.Add("Password must be at least 6 characters long.");
                    passwordTxt.BackColor = Color.LightPink;
                    allValid = false;
                }
                else if (!password.Any(char.IsDigit) || !password.Any(char.IsLetter))
                {
                    errors.Add("Password must contain both letters and numbers.");
                    passwordTxt.BackColor = Color.LightPink;
                    allValid = false;
                }
                else
                {
                    passwordTxt.BackColor = Color.White;
                }
            }

            return (allValid, string.Join("\n", errors));
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {

            var (isFormValid, formErrors) = LoginValidator.Validate(usernameTxt, passwordTxt);//validate form
            if(!isFormValid)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (isUserAuthenticated, loginMsg) = DBUtils.LoginUser(usernameTxt.Text, passwordTxt.Text);//login to db
            MessageBox.Show(string.Join("\n", loginMsg), "Validation Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            if (!isUserAuthenticated)
            {
                return;
            }

            this.Hide();//if no issues with form validaiton or db auth, login and go to dashboard
            Dashboard frm = new Dashboard();
            frm.FormClosed += (s, args) => this.Close(); // 💡 closes login form when dashboard closes

            frm.Show();

        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
