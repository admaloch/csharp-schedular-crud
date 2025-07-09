using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class MainAppointments : Form
    {
        private List<Appointment> appointments;
        public MainAppointments()
        {
            InitializeComponent();
            this.Load += MainAppointments_Load;
        }
        private void MainAppointments_Load(object sender, EventArgs e)
        {
            SetSelectedDateApptsDgv();
        }
        private DateTime GetSelectedCalendarDate()
        {
            return monthCalendar.SelectionStart.Date;
        }

        private void SetSelectedDateApptsDgv() //populate dgv
        {
            appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, GetSelectedCalendarDate());
            AppointmentUtils.SetSelectedDateApptsDgvHelper(appointments, apptDgv);
        }
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            SetSelectedDateApptsDgv();
        }
        private void addApptBtn_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            DateTime today = DateTime.Today;
            DateTime cutoffTime = today.AddHours(16).AddMinutes(30); // Today at 4:30 PM

            if (GetSelectedCalendarDate() < today || (GetSelectedCalendarDate() == today && now > cutoffTime))
            {
                MessageBox.Show("Cannot add appointment on a past date or after 5pm", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<DateTime> availableSlots = AppointmentUtils.GetAvailableApptStartTimes(GetSelectedCalendarDate(), 30, appointments);
            Console.WriteLine(availableSlots.Count);

            if (availableSlots.Count == 0)
            {
                MessageBox.Show($"{GetSelectedCalendarDate().ToString("yyyy-MM-dd")} is fully booked", "Fully Booked", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
