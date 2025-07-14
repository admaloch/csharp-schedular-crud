using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class AddAppointment : Form
    {
        private DateTime selectedDate;
        private List<Appointment> appointments;
        private int prevDurationIdx = -1;


        public AddAppointment(DateTime date)
        {
            InitializeComponent();
            selectedDate = date;
            appointments = Appointment.GetAppointmentsForUserByDate(User.CurrentUserId, selectedDate);
            SetApptTimesComboBox(30);
            if (aptTimeComboBox.Items.Count > 0)
            {
                aptTimeComboBox.SelectedIndex = 0;
            }
            SetTempDurationVals();
            this.Load += AddAppointment_Load;
        }
        private void AddAppointment_Load(object sender, EventArgs e)
        {
            dateLbl.Text = $"Date: {selectedDate.ToShortDateString()}";
            InitializeInputEvents();
            SetSelectedDateApptsDgv();



            AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);

        }
        private void SetSelectedDateApptsDgv() //populate dgv
        {
            AppointmentUtils.SetSelectedDateApptsDgvHelper(appointments, apptDgv);
        }

        private void SetTempDurationVals()
        {
            durationComboBox.Items.Clear();
            durationComboBox.Items.Add("15");
            durationComboBox.Items.Add("30");
            durationComboBox.Items.Add("45");
            durationComboBox.Items.Add("60");
            durationComboBox.SelectedIndex = 1;
        }

        private void SetApptTimesComboBox(int duration)
        {
            var apptStartTimes = AppointmentUtils2.GetAvailableApptStartTimes(selectedDate, duration, appointments);
            aptTimeComboBox.Items.Clear();

            foreach (var time in apptStartTimes)
            {
                aptTimeComboBox.Items.Add(time.ToString("hh:mm tt"));
            }

        }

        private void SetDurationComboBox()
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
            DateTime selectedSlotTime = ConvertToDateTime(selectedItem);
            DateTime thirtyMinTestVal = selectedSlotTime.AddMinutes(30);


            //test if there is a following item and that it isn't the last appt of hte day
            //determine if 60 mins is or isn't possible then update at the end
            int currentIndex = aptTimeComboBox.SelectedIndex;
            bool isSixtyminPossible = true;
            if (currentIndex >= 0 && currentIndex < aptTimeComboBox.Items.Count - 1 && selectedSlotTime.TimeOfDay != new TimeSpan(16, 30, 0))
            {
                //Console.WriteLine("apt time combo index changed");

                string nextSlotTime = aptTimeComboBox.Items[currentIndex + 1].ToString();
                DateTime nextSlotDateTime = ConvertToDateTime(nextSlotTime);


                if (nextSlotTime != null && nextSlotDateTime == thirtyMinTestVal)
                {
                    //Console.WriteLine("there is a following appt 30 mins later so 60 mins is possible ");

                    isSixtyminPossible = true;
                }
                else
                {
                    //Console.WriteLine("there is not a following appt 30 mins later so 60 mins is not possible ");

                    isSixtyminPossible = false;
                }
            }
            else
            {
                //Console.WriteLine("there is not a following appt at all so 60 mins is not possible ");

                isSixtyminPossible = false;
            }

            if (isSixtyminPossible && durationComboBox.Items.Count == 2)
            {
                //Console.WriteLine("adding 45 and 60 since 60 is possible and 15 and 30 were only options  ");

                durationComboBox.Items.Add("45");
                durationComboBox.Items.Add("60");
            }
            else if (!isSixtyminPossible && durationComboBox.Items.Count == 4)
            {
                //Console.WriteLine("removing 45 and 60 since 60 is not possible so we need to remove 45 nd 60  ");

                durationComboBox.Items.Remove("45");
                durationComboBox.Items.Remove("60");
                if (durationComboBox.SelectedIndex == 3 || durationComboBox.SelectedIndex == 4)
                {
                    durationComboBox.SelectedIndex = 1;
                }
            }
            else
            {
                //Console.WriteLine("nothing needed changing -- all good ");

            }
        }

        private DateTime ConvertToDateTime(string selectedItem)
        {
            DateTime.TryParseExact(selectedItem, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timePart);
            DateTime selectedSlotTime = selectedDate.Date.Add(timePart.TimeOfDay);
            return selectedSlotTime;
        }

        private void UpdateApptTimesOnDurationChange()
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
                SetApptTimesComboBox(changedToLongerDuration ? 60 : 30);//reset combobox times
                aptTimeComboBox.SelectedItem = initialApptItem;//set to remembered time ^
                prevDurationIdx = newDurationIdx; //update prevDuration idx with new idx val
            }
        }

        private void durationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateApptTimesOnDurationChange();
        }
        private void aptTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetDurationComboBox();
        }






        private void submitBtn_Click(object sender, EventArgs e)
        {
            var (isValid, formErrors) = AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);
            if (!isValid)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedTime = aptTimeComboBox.SelectedItem.ToString(); // e.g., "10:30 AM"
            int durationMinutes = int.Parse(durationComboBox.SelectedItem.ToString());

            // Combine date and time into a DateTime
            DateTime start = DateTime.Parse($"{selectedDate.ToShortDateString()} {selectedTime}");
            DateTime end = start.AddMinutes(durationMinutes);


            Appointment newAppointment = new Appointment
            {
                AppointmentId = 0,
                CustomerId = (int)customerComboBox.SelectedValue,
                CustomerName = customerComboBox.Text,
                Title = customerComboBox.Text,
                Type = typeTxt.Text,
                Description = descriptionTxt.Text,
                Location = locationTxt.Text,
                Contact = contactTxt.Text,
                Url = urlTxt.Text,
                Start = start,
                End = end
            };

            bool isSuccess = Appointment.InsertAppointment(newAppointment);
            if (!isSuccess)
            {
                MessageBox.Show("Failed to add new appointment", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Don't close form
            }

            MessageBox.Show("Appointment successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void InitializeInputEvents()
        {
            nameTxt.TextChanged += SharedInputChanged;
            typeTxt.TextChanged += SharedInputChanged;
            locationTxt.TextChanged += SharedInputChanged;
        }
        private void SharedInputChanged(object sender, EventArgs e)//connect inputs into shared listener
        {
            AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);
        }

    }
}
