using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    internal static class Program
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AllocConsole(); // 👈 Allocate console BEFORE running the app
            Console.WriteLine("Console initialized.");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Login loginForm = new Login();
            Application.Run(loginForm); // This blocks until login form closes
        }
    }
}
