using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class MainReports : Form
    {
        public MainReports()
        {
            InitializeComponent();
        }

        private void apptTypesBtn_Click(object sender, EventArgs e)
        {
            ApptTypesForm frm = new ApptTypesForm();
            frm.Show();
        }

        private void userScheduleBtn_Click(object sender, EventArgs e)
        {
            AllUserScheduleForm frm = new AllUserScheduleForm();
            frm.Show();
        }

        private void topCustomersBtn_Click(object sender, EventArgs e)
        {
            CustomerEngagementForm frm = new CustomerEngagementForm();
            frm.Show();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
