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
        private int appointmentId;

        private Appointment appointment;

        DateTime selectedDate;

        public ModifyAppointment(int id)
        {
            InitializeComponent();
            appointmentId = id;
            this.Load += (s, e) => PopulateDurationVals();
            AppointmentValidator.ValidateAppointment(titleTxt, typeTxt, locationTxt);
            UpdateCurrUserAppts();
            selectedDate = appointment.Start.Date;//grab selected date
            LoadCustomerComboBox();
            InitializeInputEvents();
            //validate inputs
            List<Appointment> appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, selectedDate);
            durationComboBox.SelectedIndexChanged += (s, e) => AppointmentUtils.CalcAvailableApptSlots(aptTimeComboBox, durationComboBox, selectedDate, appointments);
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            // Title
            titleTxt.Text = appointment.Title;

            // Type
            typeTxt.Text = appointment.Type;

            // Description
            descriptionTxt.Text = appointment.Description;

            // Location
            locationTxt.Text = appointment.Location;

            // Contact
            contactTxt.Text = appointment.Contact;

            // URL
            urlTxt.Text = appointment.Url;

            // Set customer combo box selection
            customerComboBox.SelectedValue = appointment.CustomerId;

            // Set duration
            int duration = (int)(appointment.End - appointment.Start).TotalMinutes;
            if (durationComboBox.Items.Contains(duration.ToString()))
            {
                durationComboBox.SelectedItem = duration.ToString();
            }
            else
            {
                durationComboBox.SelectedIndex = 0; // fallback
            }

            // Set time slot (formatted to match the items in your aptTimeComboBox, like "9:00 AM")
            string timeString = appointment.Start.ToString("h:mm tt");
            if (aptTimeComboBox.Items.Contains(timeString))
            {
                aptTimeComboBox.SelectedItem = timeString;
            }
            else
            {
                aptTimeComboBox.SelectedIndex = 0; // fallback
            }

        }

        public void UpdateCurrUserAppts()
        {
            //load customer data filtered by current user and populate the dgv with it
            appointment = Appointment.GetAppointmentById(appointmentId);
            apptDgv.DataSource = appointment;
            apptDgv.Columns["CustomerId"].Visible = false;
            apptDgv.Columns["start"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.Columns["end"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.ReadOnly = true;
            apptDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            apptDgv.RowHeadersVisible = false;
            apptDgv.MultiSelect = false;
            apptDgv.AllowUserToAddRows = false;
        }
        private void PopulateDurationVals()
        {
            durationComboBox.Items.Add("15");
            durationComboBox.Items.Add("30");
            durationComboBox.Items.Add("45");
            durationComboBox.Items.Add("60");
            durationComboBox.SelectedIndex = 1; // defaults to 30 minutes
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

        private void LoadCustomerComboBox()
        {
            List<Customer> usersCustomers = Customer.GetCustomersByUserId(CurrentUser.UserId);
            customerComboBox.DataSource = usersCustomers;
            customerComboBox.DisplayMember = "CustomerName"; // What user sees
            customerComboBox.ValueMember = "CustomerId";     // What you use in code
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
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

            bool isSuccess = Appointment.UpdateAppointment(newAppointment);
            if (!isSuccess)
            {
                MessageBox.Show("Failed to update appointment", "Db Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Don't close form
            }

            MessageBox.Show("Appointment successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
