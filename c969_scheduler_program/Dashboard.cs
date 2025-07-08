using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            OnDashboardLoad();
        }

        private void OnDashboardLoad()
        {
            usernameLbl.Text = $"Current user: {CurrentUser.UserName}";
            timeZoneLbl.Text = $"TimeZone: {Utilities.GetUserTimeZone()}";
            regionLbl.Text = $"Region: {Utilities.GetUserRegion()}";
            usernameLbl.Left = (this.ClientSize.Width - usernameLbl.Width) / 2;
            regionLbl.Left = (this.ClientSize.Width - regionLbl.Width) / 2;
            timeZoneLbl.Left = (this.ClientSize.Width - timeZoneLbl.Width) / 2;
        }

        private void customersBtn_Click(object sender, EventArgs e)
        {
            Customers frm = new Customers();
            frm.Show();
        }

        private void appointmentsBtn_Click(object sender, EventArgs e)
        {
            MainAppointments frm = new MainAppointments();
            frm.Show();
        }
    }
}
