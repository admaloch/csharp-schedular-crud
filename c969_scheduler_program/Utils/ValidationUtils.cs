using System.Windows.Forms;
using System.Drawing;

namespace c969_scheduler_program.Utils
{
    public static class ValidationUtils
    {
        public static bool SetValidationState(bool isValid, TextBox input)
        {
            input.BackColor = isValid ? Color.White : Color.LightPink;
            return isValid;
        }
    }
}
