using System;
using System.Globalization;
using System.Windows.Forms;


namespace c969_scheduler_program.Utils
{
    internal class Utilities
    {
        public static bool IsStringEmpty(string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        public static void UpdateRegionAndTimeZoneLabels(Label timeZoneLbl, Label regionLbl)
        {
            string localTimeZone = TimeZoneInfo.Local.Id;
            string region = RegionInfo.CurrentRegion.EnglishName;
            timeZoneLbl.Text = $"Timezone: {localTimeZone}";
            regionLbl.Text = $"Region: {region}";
        }

    }
}
