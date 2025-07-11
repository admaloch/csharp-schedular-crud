using c969_scheduler_program.Models;
using c969_scheduler_program.Utils; // for ValidationUtils, DBUtils, CurrentUser
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
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
            else
            {
                // Additional rules if not empty
                if (password.Length < 6)
                {
                    errors.Add("Password must be at least 6 characters long\nLa contraseña debe tener al menos 6 caracteres.");
                    passwordValid = false;
                    isValid = false;
                }

                if (!password.Any(char.IsLetter) || !password.Any(char.IsDigit))
                {
                    errors.Add("Password must contain both letters and numbers\nLa contraseña debe contener letras y números.");
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
