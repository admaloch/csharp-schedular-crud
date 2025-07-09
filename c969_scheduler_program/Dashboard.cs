using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            this.Load += MainAppointments_Load;
        }

        private void MainAppointments_Load(object sender, EventArgs e)
        {
            InitializeValues();
            CreateApptReminderAlert();
        }

        private void InitializeValues()
        {
            usernameLbl.Text = $"Current user: {CurrentUser.UserName}";
            timeZoneLbl.Text = $"TimeZone: {Utilities.GetUserTimeZone()}";
            regionLbl.Text = $"Region: {Utilities.GetUserRegion()}";
            usernameLbl.Left = (this.ClientSize.Width - usernameLbl.Width) / 2;
            regionLbl.Left = (this.ClientSize.Width - regionLbl.Width) / 2;
            timeZoneLbl.Left = (this.ClientSize.Width - timeZoneLbl.Width) / 2;
        }

        private async void CreateApptReminderAlert()
        {
            await Task.Delay(1500);
            var todaysAppts = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, DateTime.Today);
            var upcomingAppt = todaysAppts.FirstOrDefault(appt => appt.Start > DateTime.Now && appt.Start < DateTime.Now.AddMinutes(25));
            if (upcomingAppt != null)
            {
                MessageBox.Show($"Reminder!: You have an appointment at {upcomingAppt.Start.ToString("hh:mm")} with {upcomingAppt.CustomerName}", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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
