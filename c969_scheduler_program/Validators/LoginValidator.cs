using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using c969_scheduler_program.Utils;

namespace c969_scheduler_program.Validators
{
    public static class LoginValidator
    {
        public static (bool IsValid, List<string> Errors) Validate(TextBox usernameTxt, TextBox passwordTxt)
        {
            var errors = new List<string>();

            bool isValid = true;

            string username = usernameTxt.Text;
            string password = passwordTxt.Text;

            // Username validation
            bool usernameValid = ValidationUtils.SetValidationState(!string.IsNullOrWhiteSpace(username), usernameTxt);
            if (!usernameValid)
            {
                errors.Add("Username is required.");
                isValid = false;
            }

            // Password validation
            bool passwordNotEmpty = !string.IsNullOrWhiteSpace(password);
            bool passwordValid = ValidationUtils.SetValidationState(passwordNotEmpty, passwordTxt);
            if (!passwordNotEmpty)
            {
                errors.Add("Password is required.");
                isValid = false;
            }
            else
            {
                // Additional rules if not empty
                if (password.Length < 6)
                {
                    errors.Add("Password must be at least 6 characters long.");
                    passwordValid = false;
                    isValid = false;
                }

                if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                {
                    errors.Add("Password must contain both letters and numbers.");
                    passwordValid = false;
                    isValid = false;
                }

                // Apply visual feedback again if needed
                ValidationUtils.SetValidationState(passwordValid, passwordTxt);
            }

            return (isValid, errors);
        }
    }
}
