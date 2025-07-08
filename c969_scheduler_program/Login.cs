
using c969_scheduler_program.Validators;
using c969_scheduler_program.Utils;

using System;

using System.Globalization;
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
            this.Shown += Login_Shown;
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
            // Step 1: Validate form inputs (username + password)
            var (isFormValid, formErrors) = LoginValidator.Validate(usernameTxt, passwordTxt);
            if (!isFormValid)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Step 2: Try authenticating the user (DB auth + set CurrentUser)
            var (isUserAuthenticated, loginMsg) = LoginValidator.TryLogin(usernameTxt.Text, passwordTxt.Text);

            MessageBox.Show(loginMsg, "Login Message", MessageBoxButtons.OK);

            if (!isUserAuthenticated)
            {
                return;
            }

            // Step 3: Open dashboard, close login when dashboard closes
            Logger.LogLogin(CurrentUser.UserName);
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.FormClosed += (s, args) => this.Close(); // Closes login when dashboard exits
            frm.Show();
        }

        private void Login_Shown(object sender, EventArgs e)
        {
#if DEBUG
            // Auto-login user for dev mode
            string testUsername = "echen";
            string testPassword = "pw12345";

            var (isAuthenticated, message) = LoginValidator.TryLogin(testUsername, testPassword);

            if (isAuthenticated)
            {
                Logger.LogLogin(CurrentUser.UserName);
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
