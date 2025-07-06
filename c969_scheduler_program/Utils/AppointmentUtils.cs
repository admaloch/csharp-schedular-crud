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
            List<Appointment> appointments,
            Appointment appointmentToIgnore = null
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

                    bool overlaps = false;

                    foreach (var appt in appointments)
                    {
                        // Skip the appointment being modified
                        if (appointmentToIgnore != null && appt.AppointmentId == appointmentToIgnore.AppointmentId)
                            continue;

                        // Check for conflict with other appointments
                        if (startTime < appt.End && proposedEnd > appt.Start)
                        {
                            // Special case: current appointment's original start time
                            if (appointmentToIgnore != null && startTime == appointmentToIgnore.Start)
                            {
                                // If the new end extends into another appt, it's a conflict
                                if (proposedEnd > appointmentToIgnore.End)
                                {
                                    overlaps = true;
                                    break;
                                }
                            }
                            else
                            {
                                overlaps = true;
                                break;
                            }
                        }
                    }

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
