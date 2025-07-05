using c969_scheduler_program.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace c969_scheduler_program.Validators
{
    internal class AppointmentValidator
    {
        public static (bool IsValid, List<string> Errors) ValidateAppointment(
     ComboBox customerComboBox,
     TextBox nameTxt,
     TextBox typeTxt,
     ComboBox durationComboBox,
     ComboBox aptTimeComboBox,
     TextBox locationTxt)
        {
            var errors = new List<string>();
            bool isValid = true;

            // Customer selection
            bool customerValid = customerComboBox.SelectedItem != null;
            if (!customerValid)
            {
                errors.Add("Customer must be selected.");
                isValid = false;
            }

            // Appointment Title
            bool titleValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(nameTxt.Text), nameTxt);
            if (!titleValid)
            {
                errors.Add("Appointment title is required.");
                isValid = false;
            }

            // Type
            bool typeValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(typeTxt.Text), typeTxt);
            if (!typeValid)
            {
                errors.Add("Appointment type is required.");
                isValid = false;
            }

            // Duration
            bool durationValid = durationComboBox.SelectedItem != null;
            if (!durationValid)
            {
                errors.Add("Appointment duration must be selected.");
                isValid = false;
            }

            // Appointment time slot
            bool timeValid = aptTimeComboBox.SelectedItem != null;
            if (!timeValid)
            {
                errors.Add("Appointment time must be selected.");
                isValid = false;
            }

            // Location
            bool locationValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(locationTxt.Text), locationTxt);
            if (!locationValid)
            {
                errors.Add("Location is required.");
                isValid = false;
            }

            return (isValid, errors);
        }

    }
}
