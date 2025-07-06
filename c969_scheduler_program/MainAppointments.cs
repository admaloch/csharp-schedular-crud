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

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            LoadCurrApptDate();
        }


        private void addApptBtn_Click(object sender, EventArgs e)
        {
            AddAppointment frm = new AddAppointment(GenCurrDate());
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadCurrApptDate();
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
            ModifyAppointment frm = new ModifyAppointment(appointmentId);
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                LoadCurrApptDate();
            }
        }
    }
}
