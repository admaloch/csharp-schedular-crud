using c969_scheduler_program.Constants;
using System;
using System.IO;


namespace c969_scheduler_program.Utils
{
    internal class Logger
    {
        private static string path = FileLocations.LoginHistoryFile;

        public static void LogLogin(string username)
        {

            // Ensure directory exists
            string directory = Path.GetDirectoryName(path);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string entry = $"Login: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC - User: {username}";
            File.AppendAllText(path, entry + Environment.NewLine);
        }


    }
}
