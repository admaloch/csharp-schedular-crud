using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class AddAppointment : Form
    {
        private DateTime selectedDate;
        private List<Appointment> appointments;

        public AddAppointment(DateTime date)
        {
            InitializeComponent();
            selectedDate = date;
            this.Load += AddAppointment_Load;
        }
        private void AddAppointment_Load(object sender, EventArgs e)
        {
            dateLbl.Text = $"Date: {selectedDate.ToShortDateString()}";
            InitializeInputEvents();
            SetSelectedDateApptsDgv();
            AppointmentUtils.SetCustomerComboBoxVals(customerComboBox);
            AppointmentUtils.SetApptDurationComboBoxVals(durationComboBox, appointments, selectedDate);
            AppointmentUtils.CalcAvailableApptSlots(aptTimeComboBox, durationComboBox, selectedDate, appointments);
            AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);
        }
        private void SetSelectedDateApptsDgv() //populate dgv
        {
            appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, selectedDate);
            AppointmentUtils.SetSelectedDateApptsDgvHelper(appointments, apptDgv);
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

        private void durationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (appointments == null)
            {
                return;
            }

            AppointmentUtils.CalcAvailableApptSlots(aptTimeComboBox, durationComboBox, selectedDate, appointments);
        }
    }
}
