using System;
using System.IO;


namespace c969_scheduler_program.Constants
{
    public static class FileLocations
    {
        public static readonly string LoginHistoryFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "Login_History.txt");
    }
}
