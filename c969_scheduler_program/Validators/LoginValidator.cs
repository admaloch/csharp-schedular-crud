using c969_scheduler_program.Utils; // for ValidationUtils, DBUtils, CurrentUser
using System.Collections.Generic;
using System.Windows.Forms;

namespace c969_scheduler_program.Validators
{
    public static class LoginValidator
    {
        // Validate the form inputs only (UI validation)
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
                errors.Add("Username is required\nSe requiere nombre de usuario.");
                isValid = false;
            }

            // Password validation
            bool passwordNotEmpty = !string.IsNullOrWhiteSpace(password);
            bool passwordValid = ValidationUtils.SetValidationState(passwordNotEmpty, passwordTxt);
            if (!passwordNotEmpty)
            {
                errors.Add("Password is required\nSe requiere contraseña.");
                isValid = false;
            }


            return (isValid, errors);
        }


    }


}
