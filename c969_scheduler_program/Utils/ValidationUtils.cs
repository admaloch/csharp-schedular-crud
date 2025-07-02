
using System.Drawing;
using System.Windows.Forms;



namespace c969_scheduler_program.Utils
{
    internal class ValidationUtils
    {
        public static bool SetValidationState(bool condition, TextBox control)
        {
            control.BackColor = condition ? Color.White : Color.LightCoral;
            return condition;
        }
    }
}
