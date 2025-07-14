using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace c969_scheduler_program.Utils
{
    internal class AppointmentUtils2
    {
        //---- Helper funcs for add and modify appointments -- a lot of the same logic

        //look at appointments list and moditem if any -- also cur durrationComboBox value -- create a list of possible times
        //ex. if 45 or 60 min is set for durationComboBox only push out slots that can be longer than 30 min
        public static List<DateTime> GetAvailableApptStartTimes(
            DateTime selectedDate,
            int durationVal,
            List<Appointment> appointments,
            Appointment modApptItem = null
        )
        {

            var allPossibleTimes = new List<DateTime>();

            DateTime startTime = selectedDate.Date.AddHours(9);   // 9:00 AM
            DateTime endTime = selectedDate.Date.AddHours(16.5);    // 5:00 PM

            //generate all possible appointment times
            while (startTime <= endTime)
            {
                bool timeAlreadyBooked = appointments.Any(appt => appt.Start == startTime);
                bool timeIsModTime = false;

                if (modApptItem != null)
                {
                    timeIsModTime = modApptItem.Start == startTime;
                }

                if (timeAlreadyBooked && !timeIsModTime)//ignore if alredybooked but add if modInput
                {
                    startTime = startTime.AddMinutes(30);  // Don't forget to increment time
                    continue;
                }

                allPossibleTimes.Add(startTime);
                startTime = startTime.AddMinutes(30);
            }


            if (durationVal > 30)//filter results if duration is 45 or 60
            {
                for (var i = 0; i < allPossibleTimes.Count; i++)
                {
                    //check if it is 4:30pm and remove... not possible since appts need to end at 5
                    if (allPossibleTimes[i].TimeOfDay == new TimeSpan(16, 30, 0))
                    {
                        allPossibleTimes.RemoveAt(i);
                        break; // or adjust index if continuing
                    }

                    //if hte next val is not 30 mins in the future -- then 60 mins isn't possible for curr val
                    DateTime nextVal = allPossibleTimes[i + 1];
                    DateTime thirtyMinTestVal = allPossibleTimes[i].AddMinutes(30);
                    if (nextVal != null && nextVal != thirtyMinTestVal)
                    {
                        allPossibleTimes.Remove(allPossibleTimes[i]);
                    }
                }
            }

            return allPossibleTimes;
        }


        //populate apptTimesComboBox with GetAvailableApptStartTimes method above ^
        public static void SetApptTimesComboBox(int duration, ComboBox aptTimeComboBox, DateTime selectedDate, List<Appointment> appointments)
        {
            var apptStartTimes = GetAvailableApptStartTimes(selectedDate, duration, appointments);
            aptTimeComboBox.Items.Clear();

            foreach (var time in apptStartTimes)
            {
                aptTimeComboBox.Items.Add(time.ToString("hh:mm tt"));
            }
        }

        //helper for durationComboBox event listener 
        //look at some conditions to determine if the items in the apptTimesComboBox need to be updated based on durationComboBox value change
        public static void UpdateApptTimesOnDurationChange(ComboBox durationComboBox, ComboBox aptTimeComboBox, int prevDurationIdx, DateTime selectedDate, List<Appointment> appointments)
        {
            int newDurationIdx = durationComboBox.SelectedIndex;
            if (newDurationIdx == -1 || aptTimeComboBox.Items.Count == 0)
            {
                return;
            }
            //update the aptTimeComboBox only if value of durationComboBox changes from 15/30 to 45/60 and vice versa
            bool changedToShorterDuration = prevDurationIdx == 2 || prevDurationIdx == 3 && newDurationIdx == 0 || newDurationIdx == 1;
            bool changedToLongerDuration = prevDurationIdx == 0 || prevDurationIdx == 1 && newDurationIdx == 2 || newDurationIdx == 3;

            if (changedToShorterDuration || changedToLongerDuration)
            {
                string initialApptItem = aptTimeComboBox.SelectedItem.ToString();//remember prev time
                int durationRes = changedToLongerDuration ? 60 : 30;
                SetApptTimesComboBox(durationRes, aptTimeComboBox, selectedDate, appointments);//reset combobox times
                aptTimeComboBox.SelectedItem = initialApptItem;//set to remembered time ^
                prevDurationIdx = newDurationIdx; //update prevDuration idx with new idx val
            }
        }

        //helpter for aptTimesComboBox event listner
        // determine if the duractionComboBox values need to change based on the current selected appointment slot
        public static void SetDurationComboBox(ComboBox durationComboBox, ComboBox aptTimeComboBox, DateTime selectedDate)
        {
            //Console.WriteLine("set duration method ran");

            //if duration dropwdown on 45 or 60 it restricts appt slots lower than 45 min so this can't change
            bool isDurationChangePossible = aptTimeComboBox.Items.Count == 0 || durationComboBox.Items.Count == 0 || durationComboBox.SelectedIndex == 2 || durationComboBox.SelectedIndex == 3;
            if (isDurationChangePossible)
            {
                return;
            }

            //grab curr selected apt time -- convert to date to check if there is an appt in 30 min
            string selectedItem = aptTimeComboBox.SelectedItem.ToString();
            DateTime selectedSlotTime = Utilities.ConvertToDateTime(selectedItem, selectedDate);
            DateTime thirtyMinTestVal = selectedSlotTime.AddMinutes(30);


            //test if there is a following item and that it isn't the last appt of hte day
            //determine if 60 mins is or isn't possible then update at the end
            int currentIndex = aptTimeComboBox.SelectedIndex;
            bool isSixtyminPossible = true;
            if (currentIndex >= 0 && currentIndex < aptTimeComboBox.Items.Count - 1 && selectedSlotTime.TimeOfDay != new TimeSpan(16, 30, 0))
            {
                string nextSlotTime = aptTimeComboBox.Items[currentIndex + 1].ToString();
                DateTime nextSlotDateTime = Utilities.ConvertToDateTime(nextSlotTime, selectedDate);
                isSixtyminPossible = nextSlotTime != null && nextSlotDateTime == thirtyMinTestVal ? true : false;
            }
            else
            {
                isSixtyminPossible = false;
            }

            if (isSixtyminPossible && durationComboBox.Items.Count == 2)
            {
                durationComboBox.Items.Add("45");
                durationComboBox.Items.Add("60");
            }
            else if (!isSixtyminPossible && durationComboBox.Items.Count == 4)
            {
                durationComboBox.Items.Remove("45");
                durationComboBox.Items.Remove("60");
                if (durationComboBox.SelectedIndex == 3 || durationComboBox.SelectedIndex == 4)
                {
                    durationComboBox.SelectedIndex = 1;
                }
            }
        }

        //set default duration values
        public static void SetInitDurationVals(ComboBox durationComboBox)
        {
            durationComboBox.Items.Clear();
            durationComboBox.Items.Add("15");
            durationComboBox.Items.Add("30");
            durationComboBox.Items.Add("45");
            durationComboBox.Items.Add("60");
            durationComboBox.SelectedIndex = 1;
        }

    }
}
