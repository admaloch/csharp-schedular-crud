
using c969_scheduler_program.Validators;
using c969_scheduler_program.Utils;
using System;
using System.Windows.Forms;
using c969_scheduler_program.Models;



namespace c969_scheduler_program
{

    public partial class Login : Form
    {

        public Login()
        {
            InitializeComponent();
            this.Load += Login_Load;
            //this.Shown += Login_Shown;
        }
        private void Login_Load(object sender, EventArgs e) // add parameters here
        {
            usernameTxt.TextChanged += SharedInputChanged; // Fix: attach to TextChanged event (not += a method to Text)
            passwordTxt.TextChanged += SharedInputChanged;
            timeZoneLbl.Text = $"TimeZone: {Utilities.GetUserTimeZone()}";
            regionLbl.Text = $"Region: {Utilities.GetUserRegion()}";
            LoginValidator.Validate(usernameTxt, passwordTxt);
        }

        private void SharedInputChanged(object sender, EventArgs e)
        {
            LoginValidator.Validate(usernameTxt, passwordTxt);
        }


        private void loginBtn_Click(object sender, EventArgs e)
        {
            var (isFormValid, formErrors) = LoginValidator.Validate(usernameTxt, passwordTxt);
            if (!isFormValid)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (isUserAuthenticated, loginMsg) = DBUtils.TryLogin(usernameTxt.Text, passwordTxt.Text);
            MessageBox.Show(loginMsg, "Login Message", MessageBoxButtons.OK);

            if (!isUserAuthenticated)
            {
                return;
            }

            Logger.LogLogin(User.CurrentUserName);
            this.DialogResult = DialogResult.OK; // Tells Program.cs to launch Dashboard
            this.Close(); // Close the login form
        }

        private void Login_Shown(object sender, EventArgs e)
        {
#if DEBUG
            // Auto-login user for dev mode
            string testUsername = "echen";
            string testPassword = "pw12345";

            var (isAuthenticated, message) = DBUtils.TryLogin(testUsername, testPassword);

            if (isAuthenticated)
            {
                Logger.LogLogin(User.CurrentUserName);
                this.Hide();
                Dashboard dashboard = new Dashboard();
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
            else
            {
                MessageBox.Show("Auto-login failed: " + message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#endif
        }



        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}