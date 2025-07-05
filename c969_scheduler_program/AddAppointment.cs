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
        public AddAppointment()
        {
            InitializeComponent();
            this.Load += (s, e) => InitializeInputEvents();
            AppointmentValidator.ValidateAppointment(customerComboBox, nameTxt, typeTxt, durationComboBox, aptTimeComboBox, locationTxt);

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
