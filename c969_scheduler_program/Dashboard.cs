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
            Utilities.UpdateRegionAndTimeZoneLabels(timeZoneLbl, regionLbl);
            usernameLbl.Text = $"Current user: {CurrentUser.UserName}";
        }

        private void customersBtn_Click(object sender, EventArgs e)
        {
            Customers frm = new Customers();
            frm.Show();
        }
    }
}
