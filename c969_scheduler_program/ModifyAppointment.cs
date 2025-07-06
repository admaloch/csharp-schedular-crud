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

        DateTime selectedDate;

        public ModifyAppointment(Appointment appointment)
        {
            InitializeComponent(); // Must come first
            currAppointment = appointment;
            // Delay this until the form is loaded fully
            this.Load += ModifyAppointment_Load;
        }

        private void ModifyAppointment_Load(object sender, EventArgs e)
        {

            if (currAppointment == null)
            {
                MessageBox.Show("Appointment not found.");
                this.Close();
                return;
            }


            selectedDate = currAppointment.Start.Date;
            monthCalendar.SetDate(selectedDate);

            dateLbl.Text = $"Date: {currAppointment.Start}";

            SetApptDurationSelectVals();

            // Populate available slots
            SetApptSlotsVals();

            LoadCustomerComboBox();
            InitializeInputEvents();
            SetInitialInputValues(); // prefill form inputs

            LoadCurrApptData(); // If this uses currAppointment, it's now safe
        }

        private void SetApptSlotsVals()
        {
            List<Appointment> appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, selectedDate);
            AppointmentUtils.CalcAvailableApptSlots(aptTimeComboBox, durationComboBox, selectedDate, appointments, currAppointment);
        }
        private void LoadCurrApptData()
        {
            apptDgv.DataSource = new List<Appointment> { currAppointment };

            apptDgv.Columns["CustomerId"].Visible = false;
            apptDgv.Columns["Start"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.Columns["End"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.ReadOnly = true;
            apptDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            apptDgv.RowHeadersVisible = false;
            apptDgv.MultiSelect = false;
            apptDgv.AllowUserToAddRows = false;
        }
        private void SetInitialInputValues()
        {
            titleTxt.Text = currAppointment.Title;
            typeTxt.Text = currAppointment.Type;
            descriptionTxt.Text = currAppointment.Description;
            locationTxt.Text = currAppointment.Location;
            contactTxt.Text = currAppointment.Contact;
            urlTxt.Text = currAppointment.Url;

            // Set customer combo box selection
            customerComboBox.SelectedValue = currAppointment.CustomerId;

            // Set duration
            int duration = (int)(currAppointment.End - currAppointment.Start).TotalMinutes;
            if (durationComboBox.Items.Contains(duration.ToString()))
            {
                durationComboBox.SelectedItem = duration.ToString();
            }
            else
            {
                durationComboBox.SelectedIndex = 0; // fallback
            }

            string timeString = currAppointment.Start.ToString("hh:mm tt");

            bool matchFound = false;
            foreach (var item in aptTimeComboBox.Items)
            {
                if (item.ToString().Trim().Equals(timeString, StringComparison.InvariantCultureIgnoreCase))
                {
                    aptTimeComboBox.SelectedItem = item;
                    matchFound = true;
                    break;
                }
            }

            if (!matchFound && aptTimeComboBox.Items.Count > 0)
            {
                aptTimeComboBox.SelectedIndex = 0; // fallback
            }

        }
        private void SetApptDurationSelectVals()
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
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            selectedDate = monthCalendar.SelectionStart.Date;
            MessageBox.Show(selectedDate.ToString());
            SetApptSlotsVals();

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
                //MessageBox.Show("Failed to update appointment", "Db Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Don't close form
            }

            MessageBox.Show($"Appointment changed to {start}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void durationComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetApptSlotsVals();
        }
    }
}
