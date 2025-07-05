using c969_scheduler_program.Models;
using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class MainAppointments : Form
    {
        public MainAppointments()
        {
            InitializeComponent();
            LoadCurrApptDate();

        }

        private DateTime GenCurrDate()
        {
            return monthCalendar.SelectionStart.Date;
        }

        private void LoadCurrApptDate()
        {
            var appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, GenCurrDate());
            apptDgv.DataSource = appointments;
            apptDgv.Columns["CustomerId"].Visible = false;
            apptDgv.Columns["start"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.Columns["end"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.ReadOnly = true;
            apptDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            apptDgv.RowHeadersVisible = false;
            apptDgv.MultiSelect = false;
            apptDgv.AllowUserToAddRows = false;
        }


        private void addApptBtn_Click(object sender, EventArgs e)
        {
            AddAppointment frm = new AddAppointment(GenCurrDate());
            var result = frm.ShowDialog(); // ✅ ONLY this, no .Show()

            if (result == DialogResult.OK)
            {
                LoadCurrApptDate(); // ✅ Will now run correctly after dialog closes
            }
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadCurrApptDate();
        }
    }
}
