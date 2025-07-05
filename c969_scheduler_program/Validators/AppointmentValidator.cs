using c969_scheduler_program.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace c969_scheduler_program.Validators
{
    internal class AppointmentValidator
    {
        public static (bool IsValid, List<string> Errors) ValidateAppointment(
             TextBox nameTxt,
             TextBox typeTxt,
             TextBox locationTxt
            )
        {
            var errors = new List<string>();
            bool isValid = true;


            // Appointment Title
            bool titleValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(nameTxt.Text), nameTxt);
            if (!titleValid)
            {
                errors.Add("Title is required.");
                isValid = false;
            }

            // Type
            bool typeValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(typeTxt.Text), typeTxt);
            if (!typeValid)
            {
                errors.Add("Appointment type is required.");
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
