using c969_scheduler_program.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace c969_scheduler_program.Utils
{
    internal class NewAppointmentUtils
    {
        public static void CalcAvailableApptSlots(
            ComboBox aptTimeComboBox,
            ComboBox durationComboBox,
            DateTime selectedDate,
            List<Appointment> appointments,
            Appointment modFormAppt = null
        )
        {

            //aptTimeComboBox.Items.Clear();
            int currduration = GetSelectedDuration(durationComboBox);//15, 30, 45, 60
            DateTime startTime = selectedDate.AddHours(9);  // 9:00 AM
            DateTime endTime = selectedDate.AddHours(17);   // 5:00 PM

            Console.WriteLine($"current duration: {currduration}");



            while (startTime.AddMinutes(currduration) <= endTime)
            {
                string targetStart = startTime.ToString("hh:mm tt");
                int index = appointments.FindIndex(appt => appt.Start.TimeOfDay == startTime.TimeOfDay);
                if (index == -1) //there is a match
                {
                    TimeSpan calcOrigDuration = appointments[index].End - appointments[index].Start;
                    int origDuration = (int)calcOrigDuration.TotalMinutes;

                    //if (currduration > 30 && origDuration <= 30)
                    //{
                    //    Console.WriteLine($"new val higher orig: {calcOrigDuration} selected: {currduration}");
                    //}
                    if (modFormAppt != null) //there is a currval
                    {

                    }
                    aptTimeComboBox.Items.Add(startTime.ToString("hh:mm tt"));

                }


                startTime = startTime.AddMinutes(30);
            }


        }




        private static int GetSelectedDuration(ComboBox durationComboBox)
        {
            return int.TryParse(durationComboBox.SelectedItem?.ToString(), out int duration) ? duration : 0;
        }
    }
}
