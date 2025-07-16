using c969_scheduler_program.Utils;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;


namespace c969_scheduler_program.Validators
{
    internal class CustomerValidator
    {
        public static (bool IsValid, List<string> Errors) ValidateCustomer(
               TextBox nameTxt,
               TextBox addressTxt,
               TextBox cityTxt,
               TextBox countryTxt,
               TextBox zipTxt,
               TextBox phoneTxt)
        {

            var errors = new List<string>();
            bool isValid = true;


            // Name
            bool nameValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(nameTxt.Text), nameTxt);
            if (!nameValid)
            {
                errors.Add("Name is required.");
                isValid = false;
            }

            // Address
            bool addressValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(addressTxt.Text), addressTxt);
            if (!addressValid)
            {
                errors.Add("Address is required.");
                isValid = false;
            }

            // City
            bool cityValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(cityTxt.Text), cityTxt);
            if (!cityValid)
            {
                errors.Add("City is required.");
                isValid = false;
            }

            // Country
            bool countryValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(countryTxt.Text), countryTxt);
            if (!countryValid)
            {
                errors.Add("Country is required.");
                isValid = false;
            }

            // Postal/Zip Code
            bool zipValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(zipTxt.Text), zipTxt);
            if (!zipValid)
            {
                errors.Add("Postal Code is required.");
                isValid = false;
            }

            // Phone
            string phone = phoneTxt.Text.Trim();
            bool phoneNotEmpty = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(phone), phoneTxt);
            if (!phoneNotEmpty)
            {
                errors.Add("Phone number is required.");
                isValid = false;
            }
            else
            {
                // Regex: only digits and dashes allowed
                bool phoneValid = Regex.IsMatch(phone, @"^\d[\d-]*\d$") && phone.Contains("-");
                ValidationUtils.SetValidationState(phoneValid, phoneTxt);

                if (!phoneValid)
                {
                    errors.Add("Phone number must contain only digits and dashes.");
                    isValid = false;
                }
            }

            return (isValid, errors);
        }
    }
}
