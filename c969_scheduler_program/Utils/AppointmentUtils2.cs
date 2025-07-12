using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c969_scheduler_program.Utils
{
    internal class AppointmentUtils2
    {
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
            foreach (var time in allPossibleTimes)
            {
                Console.WriteLine(time.ToString());
            }
            return allPossibleTimes;
        }
    }
}
