using c969_scheduler_program;
using System;
using System.Windows.Forms;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        Login loginForm = new Login();
        if (loginForm.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new Dashboard()); // Dashboard becomes root form
        }
    }
}
