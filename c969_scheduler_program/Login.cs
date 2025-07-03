using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
            string localTimeZone = TimeZoneInfo.Local.Id;
            string region = RegionInfo.CurrentRegion.EnglishName;

            timeZoneLbl.Text = $"Timezone: {localTimeZone}";
            regionLbl.Text = $"Region: {region}";
            LoginValidator.Validate(usernameTxt, passwordTxt);
        }

        public void SharedInputChanged(object sender, EventArgs e)
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
            this.Hide();
            Dashboard frm = new Dashboard();
            frm.FormClosed += (s, args) => this.Close(); // Closes login when dashboard exits
            frm.Show();
        }


        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
