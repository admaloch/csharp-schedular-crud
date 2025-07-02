using System.Windows.Forms;


namespace c969_scheduler_program.Utils
{
    internal class Utilities
    {
        public static bool IsInputEmpty(TextBox input)
        {
            return string.IsNullOrWhiteSpace(input.Text);
        }

    }
}
