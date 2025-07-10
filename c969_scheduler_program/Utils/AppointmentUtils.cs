using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace c969_scheduler_program.Utils

{
    public static class AppointmentUtils
    {
        public static void CalcAvailableApptSlots(
         ComboBox aptTimeComboBox,
         ComboBox durationComboBox,
         DateTime selectedDate,
         List<Appointment> appointments,
         Appointment appointmentToIgnore = null
    )
        {
            aptTimeComboBox.Items.Clear();

            if (!int.TryParse(durationComboBox.SelectedItem?.ToString(), out int durationMinutes) || durationMinutes <= 0)
                return;

            var availableSlots = GetAvailableApptStartTimes(selectedDate, durationMinutes, appointments, appointmentToIgnore);

            foreach (var time in availableSlots)
            {
                aptTimeComboBox.Items.Add(time.ToString("hh:mm tt"));
            }

            if (aptTimeComboBox.Items.Count > 0)
                aptTimeComboBox.SelectedIndex = 0;
        }



        public static List<DateTime> GetAvailableApptStartTimes(
                DateTime selectedDate,
                int durationMinutes,
                List<Appointment> appointments,
                Appointment appointmentToIgnore = null
            )
        {
            var availableTimes = new List<DateTime>();

            DateTime startTime = selectedDate.Date.AddHours(9);   // 9:00 AM
            DateTime endTime = selectedDate.Date.AddHours(17);    // 5:00 PM

            while (startTime.AddMinutes(durationMinutes) <= endTime)
            {
                if (startTime.Minute == 0 || startTime.Minute == 30)
                {
                    DateTime proposedEnd = startTime.AddMinutes(durationMinutes);
                    bool conflict = appointments.Any(appt =>
                        (appointmentToIgnore == null || appt.AppointmentId != appointmentToIgnore.AppointmentId) &&
                        (
                            // Special case: allow keeping the same start time if extending doesn’t cause conflict
                            startTime == appointmentToIgnore?.Start
                                ? proposedEnd > appointmentToIgnore.End
                                : startTime < appt.End && proposedEnd > appt.Start
                        )
                    );

                    // Skip if it's the current day and time has already passed
                    if (selectedDate.Date == DateTime.Now.Date && startTime <= DateTime.Now)
                        conflict = true;

                    if (!conflict)
                        availableTimes.Add(startTime);
                }

                startTime = startTime.AddMinutes(30);
            }

            return availableTimes;
        }


        public static void SetApptDurationComboBoxVals(ComboBox comboBox, List<Appointment> appointments, DateTime selectedDate)
        {
            if (appointments == null)
            {
                return;
            }

            comboBox.Items.Clear();//set initial values
            comboBox.Items.Add("15");
            comboBox.Items.Add("30");

            // Check if theres room for appts longer than 30 mins
            bool hasLongerSlot =
                AppointmentUtils.GetAvailableApptStartTimes(selectedDate, 60, appointments).Any();

            if (hasLongerSlot)
            {
                comboBox.Items.Add("45");
                comboBox.Items.Add("60");
            }
            comboBox.SelectedIndex = 1;
        }

        public static void SetSelectedDateApptsDgvHelper(List<Appointment> appointments, DataGridView dgv) //populate dgv
        {

            dgv.DataSource = appointments;
            dgv.Columns["CustomerId"].Visible = false;
            dgv.Columns["AppointmentId"].HeaderText = "Id";
            dgv.Columns["CustomerName"].HeaderText = "Name";
            dgv.Columns["start"].DefaultCellStyle.Format = "h:mm tt";
            dgv.Columns["end"].DefaultCellStyle.Format = "h:mm tt";
            dgv.ReadOnly = true;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.RowHeadersVisible = false;
            dgv.MultiSelect = false;
            dgv.AllowUserToAddRows = false;

            if (appointments.Count > 1)
            {
                // Gray out past appointments
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    if (row.DataBoundItem is Appointment appt && appt.Start < DateTime.Now)
                    {
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                        row.DefaultCellStyle.Font = new Font(dgv.Font, FontStyle.Italic);
                        row.Tag = "past"; // You can use this tag later to block selection/modification
                    }
                }
            }

            dgv.ClearSelection();
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        }

        public static void SetCustomerComboBoxVals(ComboBox comboBox)
        {
            List<Customer> allCustomers = Customer.GetAllCustomers();
            comboBox.DataSource = allCustomers;
            comboBox.DisplayMember = "CustomerName";
            comboBox.ValueMember = "CustomerId";
        }


    }


}
