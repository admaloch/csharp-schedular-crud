using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using System;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class MainAppointments : Form
    {
        public MainAppointments()
        {
            InitializeComponent();
            SetCurrApptDgv();
        }
        private DateTime GetSelectedCalendarDate()
        {
            return monthCalendar.SelectionStart.Date;
        }
        private void SetCurrApptDgv()
        {
            var appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, GetSelectedCalendarDate());
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
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SetCurrApptDgv();
        }
        private void addApptBtn_Click(object sender, EventArgs e)
        {
            AddAppointment frm = new AddAppointment(GetSelectedCalendarDate());
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetCurrApptDgv();
            }
        }
        private void modApptBtn_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsRowSelected(apptDgv))
            {
                MessageBox.Show("Please select a customer to modify.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int appointmentId = Utilities.GrabDgvRowId(apptDgv);
            Appointment currAppointment = Appointment.GetAppointmentById(appointmentId);

            ModifyAppointment frm = new ModifyAppointment(currAppointment);
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetCurrApptDgv();
            }
        }
        private void deleteApptBtn_Click(object sender, EventArgs e)
        {
            if (!Utilities.IsRowSelected(apptDgv))
            {
                MessageBox.Show("Please select a customer to modify.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int appointmentId = Utilities.GrabDgvRowId(apptDgv);
            var result = Appointment.DeleteAppointment(appointmentId);
            if (!result)
            {
                MessageBox.Show("Unable to delete appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Appointment deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetCurrApptDgv();

        }
    }
}
