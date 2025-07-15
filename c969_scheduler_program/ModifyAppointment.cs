using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class ModifyAppointment : Form
    {
        private Appointment currAppointment;
        private DateTime selectedDate;
        private DateTime initialDate;
        private List<Appointment> appointments;
        private int prevDurationIdx = -1;
        private bool _isFormLoaded = false;
        private DateTime currSelectedApptSlot;

        public ModifyAppointment(Appointment appointment)
        {
            InitializeComponent(); // Must come first
            currAppointment = appointment;
            selectedDate = currAppointment.Start.Date;
            initialDate = currAppointment.Start.Date;
            appointments = Appointment.GetAppointmentsForUserByDate(User.CurrentUserId, selectedDate);
            monthCalendar.SetDate(selectedDate);
            AppointmentUtils.SetApptTimesComboBox(
                Utilities.CalculateDurationMinutes(currAppointment.Start, currAppointment.End),
                aptTimeComboBox,
                selectedDate,
                appointments,
                selectedDate == initialDate ? currAppointment : null
            );//set initial values for aptComboBox
            this.Load += ModifyAppointment_Load;
        }

        private void ModifyAppointment_Load(object sender, EventArgs e)
        {
            CheckIfCurrentAppt();
            InitializeInputEvents();
            var currApptAsList = new List<Appointment> { currAppointment };//dgv method below expects a list 
            AppointmentUtils.SetSelectedDateApptsDgvHelper(currApptAsList, apptDgv);//populate appointments dgv
            AppointmentUtils.SetCustomerComboBoxVals(customerComboBox); //populate customer dropdown
            SetInitialInputValues();
            currSelectedApptSlot = DateTime.Parse(aptTimeComboBox.SelectedItem.ToString());
            AppointmentUtils.SetDurationComboBox(durationComboBox, aptTimeComboBox, selectedDate);

            _isFormLoaded = true;
        }



        private void CheckIfCurrentAppt()
        {
            if (currAppointment == null)//check if currAppointment exists
            {
                MessageBox.Show("Appointment not found.");
                this.Close();
                return;
            }
        }

        private void SetInitialInputValues()
        {
            if (currAppointment == null)
            {
                return;
            }
            dateLbl.Text = $"Date: {currAppointment.Start}";

            titleTxt.Text = currAppointment.Title;
            typeTxt.Text = currAppointment.Type;
            descriptionTxt.Text = currAppointment.Description;
            locationTxt.Text = currAppointment.Location;
            contactTxt.Text = currAppointment.Contact;
            urlTxt.Text = currAppointment.Url;

            durationComboBox.Items.Add("15");
            durationComboBox.Items.Add("30");
            durationComboBox.Items.Add("45");
            durationComboBox.Items.Add("60");

            // Set customer combo box selection
            customerComboBox.SelectedValue = currAppointment.CustomerId;

            // Set duration
            int duration = Utilities.CalculateDurationMinutes(currAppointment.Start, currAppointment.End);
            if (durationComboBox.Items.Contains(duration.ToString()))
            {
                durationComboBox.SelectedItem = duration.ToString();
            }
            else
            {
                durationComboBox.SelectedIndex = 0;
            }

            string timeString = currAppointment.Start.ToString("hh:mm tt");

            foreach (var item in aptTimeComboBox.Items)
            {
                if (item.ToString().Trim().Equals(timeString, StringComparison.InvariantCultureIgnoreCase))
                {
                    aptTimeComboBox.SelectedItem = item;
                    break;
                }
            }

        }
        private void InitializeInputEvents()
        {
            titleTxt.TextChanged += SharedInputChanged;
            typeTxt.TextChanged += SharedInputChanged;
            locationTxt.TextChanged += SharedInputChanged;
        }
        private void SharedInputChanged(object sender, EventArgs e)//connect inputs into shared listener
        {
            AppointmentValidator.ValidateAppointment(titleTxt, typeTxt, locationTxt);
        }

        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            if (!_isFormLoaded) return;

            selectedDate = monthCalendar.SelectionStart.Date;

            appointments = Appointment.GetAppointmentsForUserByDate(User.CurrentUserId, selectedDate);
            if (appointments == null)
            {
                return;
            }
            AppointmentUtils.SetApptTimesComboBox(
                Utilities.CalculateDurationMinutes(currAppointment.Start, currAppointment.End),
                aptTimeComboBox,
                selectedDate,
                appointments,
                selectedDate == initialDate ? currAppointment : null
            );
            if (aptTimeComboBox != null && aptTimeComboBox.Items.Count > 0)
            {
                DateTime closestTime = AppointmentUtils.FindClosestAvailableTime(currSelectedApptSlot, aptTimeComboBox);
                aptTimeComboBox.SelectedItem = closestTime.ToString("hh:mm tt");
            }
            else
            {
                MessageBox.Show("No appointments available on this date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void durationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFormLoaded) return;
            AppointmentUtils.UpdateApptTimesOnDurationChange(
                durationComboBox,
                aptTimeComboBox,
                prevDurationIdx,
                selectedDate,
                appointments,
                selectedDate == initialDate ? currAppointment : null
            );
        }

        private void aptTimeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isFormLoaded) return;
            AppointmentUtils.SetDurationComboBox(durationComboBox, aptTimeComboBox, selectedDate);
            currSelectedApptSlot = DateTime.Parse(aptTimeComboBox.SelectedItem.ToString());

        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            if (aptTimeComboBox.Items.Count == 0)//if combobox is empty.. all appts are full
            {
                MessageBox.Show("There are no available appointments on this day", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var (isValid, formErrors) = AppointmentValidator.ValidateAppointment(titleTxt, typeTxt, locationTxt);
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
                AppointmentId = currAppointment.AppointmentId,
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

            bool isSuccess = Appointment.UpdateAppointment(newAppointment);
            if (!isSuccess)
            {
                MessageBox.Show("Failed to update appointment", "Db Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Don't close form
            }

            MessageBox.Show($"Appointment changed to {start}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }


        private void exitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
