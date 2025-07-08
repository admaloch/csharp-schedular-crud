using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class MainAppointments : Form
    {
        public MainAppointments()
        {
            InitializeComponent();
            this.Load += MainAppointments_Load;
        }
        private void MainAppointments_Load(object sender, EventArgs e)
        {
            SetSelectedDateApptsDgv();
            apptDgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        private DateTime GetSelectedCalendarDate()
        {
            return monthCalendar.SelectionStart.Date;
        }
        private void SetSelectedDateApptsDgv() //populate dgv
        {
            var appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, GetSelectedCalendarDate());
            apptDgv.DataSource = appointments;
            apptDgv.Columns["CustomerId"].Visible = false;
            apptDgv.Columns["AppointmentId"].HeaderText = "Id";
            apptDgv.Columns["CustomerName"].HeaderText = "Name";
            apptDgv.Columns["start"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.Columns["end"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.ReadOnly = true;
            apptDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            apptDgv.RowHeadersVisible = false;
            apptDgv.MultiSelect = false;
            apptDgv.AllowUserToAddRows = false;

            // Gray out past appointments
            foreach (DataGridViewRow row in apptDgv.Rows)
            {
                if (row.DataBoundItem is Appointment appt && appt.Start < DateTime.Now)
                {
                    row.DefaultCellStyle.ForeColor = Color.Gray;
                    row.DefaultCellStyle.BackColor = Color.LightGray;
                    row.DefaultCellStyle.Font = new Font(apptDgv.Font, FontStyle.Italic);
                    row.Tag = "past"; // You can use this tag later to block selection/modification
                }
            }
            apptDgv.ClearSelection();

        }
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SetSelectedDateApptsDgv();
        }
        private void addApptBtn_Click(object sender, EventArgs e)
        {
            if (GetSelectedCalendarDate() < DateTime.Today)
            {
                MessageBox.Show("Cannot add appointment on a past date.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            AddAppointment frm = new AddAppointment(GetSelectedCalendarDate());
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetSelectedDateApptsDgv();
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

            if (currAppointment.Start < DateTime.Now)
            {
                MessageBox.Show("Past appointments cannot be modified.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ModifyAppointment frm = new ModifyAppointment(currAppointment);
            var result = frm.ShowDialog();

            if (result == DialogResult.OK)
            {
                SetSelectedDateApptsDgv();
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
            Appointment currAppointment = Appointment.GetAppointmentById(appointmentId);
            if (currAppointment.Start < DateTime.Now)
            {
                MessageBox.Show("Cannot delete appointment on a past date.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var result = Appointment.DeleteAppointment(appointmentId);
            if (!result)
            {
                MessageBox.Show("Unable to delete appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            MessageBox.Show($"Appointment deleted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            SetSelectedDateApptsDgv();
        }
    }
}
