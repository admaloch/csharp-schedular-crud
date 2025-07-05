using c969_scheduler_program.Models;
using c969_scheduler_program.Utils;
using c969_scheduler_program.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class AddAppointment : Form
    {
        private DateTime selectedDate;

        public AddAppointment(DateTime date)
        {
            InitializeComponent();
            selectedDate = date;
            this.Load += (s, e) => InitializeInputEvents();
            AppointmentValidator.ValidateAppointment(customerComboBox, nameTxt, typeTxt, durationComboBox, aptTimeComboBox, locationTxt);
            LoadCurrApptDate();
            LoadCustomerComboBox();
        }
        public void LoadCurrApptDate()
        {
            var appointments = Appointment.GetAppointmentsForUserByDate(CurrentUser.UserId, selectedDate);
            apptDgv.DataSource = appointments;
            apptDgv.Columns["UserId"].Visible = false;
            apptDgv.Columns["CustomerId"].Visible = false;
            apptDgv.Columns["lastUpdateBy"].Visible = false;
            apptDgv.Columns["lastUpdate"].Visible = false;
            apptDgv.Columns["start"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.Columns["end"].DefaultCellStyle.Format = "h:mm tt";
            apptDgv.ReadOnly = true;
            apptDgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            apptDgv.RowHeadersVisible = false;
            apptDgv.MultiSelect = false;
            apptDgv.AllowUserToAddRows = false;
        }
        private void InitializeInputEvents()
        {
            //nameTxt.TextChanged += SharedInputChanged;
            durationComboBox.Items.Add("15");
            durationComboBox.Items.Add("30");
            durationComboBox.Items.Add("45");
            durationComboBox.Items.Add("60");
            durationComboBox.SelectedIndex = 1; // defaults to 30 minutes


        }
        private void SharedInputChanged(object sender, EventArgs e)//connect inputs into shared listener
        {
            AppointmentValidator.ValidateAppointment(customerComboBox, nameTxt, typeTxt, durationComboBox, aptTimeComboBox, locationTxt);
        }

        private void LoadCustomerComboBox()
        {
            List<Customer> usersCustomers = Customer.GetCustomersByUserId(CurrentUser.UserId);

            customerComboBox.DataSource = usersCustomers;
            customerComboBox.DisplayMember = "CustomerName"; // What user sees
            customerComboBox.ValueMember = "CustomerId";     // What you use in code
        }


        private List<Customer> GrabUsersCustomers(int userId)
        {
            List<Customer> usersCustomers = Customer.GetCustomersByUserId(Utilities.GrabDgvRowId(apptDgv));
            return usersCustomers;
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var (isValid, formErrors) = AppointmentValidator.ValidateAppointment(customerComboBox, nameTxt, typeTxt, durationComboBox, aptTimeComboBox, locationTxt);

            if (!isValid)
            {
                MessageBox.Show(string.Join("\n", formErrors), "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show("Appointment successfully added!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
