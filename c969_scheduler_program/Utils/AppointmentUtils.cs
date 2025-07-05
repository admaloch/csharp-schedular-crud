using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
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
            List<Appointment> appointments
            )
        {
            aptTimeComboBox.Items.Clear();

            // Get selected duration (e.g., 15, 30, 60 minutes)
            if (!int.TryParse(durationComboBox.SelectedItem?.ToString(), out int durationMinutes) || durationMinutes <= 0)
                return;

            // Define business hours
            DateTime startTime = selectedDate.AddHours(9);  // 9:00 AM
            DateTime endTime = selectedDate.AddHours(17);   // 5:00 PM

            // Start on the hour or half-hour only
            while (startTime.AddMinutes(durationMinutes) <= endTime)
            {
                if (startTime.Minute == 0 || startTime.Minute == 30)
                {
                    DateTime proposedEnd = startTime.AddMinutes(durationMinutes);

                    bool overlaps = appointments.Any(appt =>
                        (startTime < appt.End && proposedEnd > appt.Start)
                    );

                    if (!overlaps)
                    {
                        aptTimeComboBox.Items.Add(startTime.ToString("hh:mm tt"));
                    }
                }

                startTime = startTime.AddMinutes(30); // Only advance by 30-minute blocks
            }

            if (aptTimeComboBox.Items.Count > 0)
                aptTimeComboBox.SelectedIndex = 0;
        }

    }
}
