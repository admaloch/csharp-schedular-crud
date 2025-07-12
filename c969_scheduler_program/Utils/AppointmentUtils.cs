using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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

            if (appointments == null)
            {
                Console.WriteLine("Calc available appt slots failed becuase of no appointments list");
                return;
            }

            string modItemVal = "";
            if (appointmentToIgnore != null)
            {
                modItemVal = appointmentToIgnore.Start.ToString("hh:mm tt");
            }

            string manuallySelectedVal = "";
            if (aptTimeComboBox.SelectedIndex > 0)
            {
                manuallySelectedVal = aptTimeComboBox.Items[aptTimeComboBox.SelectedIndex].ToString();
            }

            aptTimeComboBox.Items.Clear();

            if (!int.TryParse(durationComboBox.SelectedItem?.ToString(), out int durationMinutes) || durationMinutes <= 0)
                return;

            var availableSlots = GetAvailableApptStartTimes(selectedDate, durationMinutes, appointments, appointmentToIgnore);

            foreach (var time in availableSlots)
            {
                aptTimeComboBox.Items.Add(time.ToString("hh:mm tt"));
            }


            if (!string.IsNullOrEmpty(manuallySelectedVal) && manuallySelectedVal != modItemVal)
            {
                aptTimeComboBox.SelectedItem = manuallySelectedVal;
                return;
            }

            if (!string.IsNullOrEmpty(modItemVal))
            {
                Console.WriteLine($"went to mod appt time");
                aptTimeComboBox.SelectedItem = modItemVal;
                return;
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

        public static void SetApptDurationComboBoxVals2(ComboBox durationComboBox, ComboBox apptComboBox, List<Appointment> appointments, DateTime selectedDate)
        {

            if (appointments == null)
            {
                Console.WriteLine("Set appt duration failed because of no appointments list");
                return;
            }

            // Store current selection
            var currSelectedDuration = durationComboBox.SelectedItem?.ToString();
            var currAppt = DateTime.ParseExact(apptComboBox.SelectedItem.ToString(), "h:mm tt", CultureInfo.InvariantCulture);
            int nextIndex = -1;
            var nextAppt = currAppt;//set temporarily to currAppt



            if (apptComboBox.SelectedIndex + 1 < apptComboBox.Items.Count)
            {
                nextIndex = apptComboBox.SelectedIndex + 1;
                nextAppt = DateTime.ParseExact(apptComboBox.Items[nextIndex].ToString(), "h:mm tt", CultureInfo.InvariantCulture);
            }

            durationComboBox.BeginUpdate();
            try
            {
                durationComboBox.Items.Clear();
                durationComboBox.Items.Add("15");
                durationComboBox.Items.Add("30");

                if (nextIndex != -1 && currAppt.AddMinutes(30) == nextAppt)
                {
                    durationComboBox.Items.Add("45");
                    durationComboBox.Items.Add("60");
                }

                // Restore selection if possible
                //durationComboBox.SelectedIndex =
                //    currSelectedDuration != null
                //    && durationComboBox.Items.Contains(currSelectedDuration)
                //        ? durationComboBox.Items.IndexOf(currSelectedDuration)
                //        : 1;

                if (currSelectedDuration != null
                    && durationComboBox.Items.Contains(currSelectedDuration))
                {
                    durationComboBox.SelectedIndex = durationComboBox.Items.IndexOf(currSelectedDuration);
                    Console.WriteLine($"selected Duration to previous val: {currSelectedDuration}");
                }
                else
                {
                    durationComboBox.SelectedIndex = 1;
                    Console.WriteLine("Selected druation : Default to idx 1");
                }
            }
            finally
            {
                durationComboBox.EndUpdate();
            }
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
