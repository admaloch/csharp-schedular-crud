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

            int durationMinutes = GetSelectedDuration(durationComboBox);
            if (durationMinutes <= 0) return;

            DateTime startTime = selectedDate.AddHours(9);  // 9:00 AM
            DateTime endTime = selectedDate.AddHours(17);   // 5:00 PM

            while (startTime.AddMinutes(durationMinutes) <= endTime)
            {
                if (IsOnHalfHourMark(startTime))
                {
                    DateTime proposedEnd = startTime.AddMinutes(durationMinutes);

                    if (!IsConflicting(startTime, proposedEnd, appointments, appointmentToIgnore))
                    {
                        aptTimeComboBox.Items.Add(startTime.ToString("hh:mm tt"));
                    }
                }

                startTime = startTime.AddMinutes(30);
            }

            if (aptTimeComboBox.Items.Count > 0)
                aptTimeComboBox.SelectedIndex = 0;
        }

        private static int GetSelectedDuration(ComboBox durationComboBox)
        {
            return int.TryParse(durationComboBox.SelectedItem?.ToString(), out int duration) ? duration : 0;
        }

        private static bool IsOnHalfHourMark(DateTime time)
        {
            return time.Minute == 0 || time.Minute == 30;
        }

        private static bool IsConflicting(
            DateTime start,
            DateTime end,
            List<Appointment> appointments,
            Appointment appointmentToIgnore)
        {
            foreach (var appt in appointments)
            {
                // Skip the appointment being modified
                if (appointmentToIgnore != null && appt.AppointmentId == appointmentToIgnore.AppointmentId)
                    continue;

                bool overlap = start < appt.End && end > appt.Start;

                if (overlap)
                {
                    // Special case: we're keeping the same start time as the original
                    if (appointmentToIgnore != null && start == appointmentToIgnore.Start)
                    {
                        // Conflict only if the new end goes past the original
                        if (end > appointmentToIgnore.End)
                            return true;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            return false;
        }


    }


}
