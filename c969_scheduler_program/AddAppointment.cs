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
            this.Load += (s, e) => PopulateDurationVals();
            AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);
            UpdateCurrUserAppts();
            LoadCustomerComboBox();
            InitializeInputEvents();
            //validate inputs
            durationComboBox.SelectedIndexChanged += (s, e) => AppointmentUtils.CalcAvailableApptSlots(aptTimeComboBox, durationComboBox, selectedDate, appointments);
        }
        public void UpdateCurrUserAppts()
        {
            //load customer data filtered by current user and populate the dgv with it
            appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, selectedDate);
            apptDgv.DataSource = appointments;
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
            nameTxt.TextChanged += SharedInputChanged;
            typeTxt.TextChanged += SharedInputChanged;
            locationTxt.TextChanged += SharedInputChanged;
        }

        private void SharedInputChanged(object sender, EventArgs e)//connect inputs into shared listener
        {
            AppointmentValidator.ValidateAppointment(nameTxt, typeTxt, locationTxt);
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
    }
}
