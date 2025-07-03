

using System;

namespace c969_scheduler_program.Models
{
    public static class CurrentUser
    {
        public static int UserId { get; set; }
        public static string UserName { get; set; }
        public static string TimeZone { get; set; }
        public static string Region { get; set; }
        public static string Language { get; set; }

        public static void SetUser(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
            TimeZone = TimeZoneInfo.Local.Id;
            Region = System.Globalization.RegionInfo.CurrentRegion.EnglishName;
            Language = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        }
    }
}
