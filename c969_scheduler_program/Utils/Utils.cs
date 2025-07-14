using System;
using System.Collections.Generic;
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

        public static DateTime ConvertToUTC(DateTime localDateTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(localDateTime, TimeZoneInfo.Local);
        }

        public static DateTime ConvertToLocalTime(DateTime utcDateTime)
        {
            // Force all non-UTC to UTC to prevent conversion error
            if (utcDateTime.Kind != DateTimeKind.Utc)
            {
                utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);
            }

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, TimeZoneInfo.Local);
        }


        public static string GetUserTimeZone()
        {
            return TimeZoneInfo.Local.Id;
        }
        public static string GetUserTimeZoneAbbreviation()
        {
            var map = new Dictionary<string, string>
            {
                { "Central Standard Time", "CST" },
                { "Central Daylight Time", "CDT" },
                { "Pacific Standard Time", "PST" },
                { "Pacific Daylight Time", "PDT" },
                { "Eastern Standard Time", "EST" },
                { "Eastern Daylight Time", "EDT" },
                // Add more as needed
            };

            TimeZoneInfo localZone = TimeZoneInfo.Local;
            string abbreviation;

            if (map.TryGetValue(localZone.StandardName, out abbreviation) ||
                map.TryGetValue(localZone.DaylightName, out abbreviation))
            {
                return abbreviation;
            }

            return localZone.StandardName; // fallback
        }
        public static string GetUserRegion()
        {
            return RegionInfo.CurrentRegion.EnglishName;
        }

        public static bool IsRowSelected(DataGridView dgv)//util for determining if a dgv row is selected
        {
            if (dgv.SelectedRows.Count > 0 && !dgv.SelectedRows[0].IsNewRow)
            {
                return true;
            }
            return false;
        }
        public static int GrabDgvRowId(DataGridView dgv)//util for taking dgv and grabbing the id val.. always first column
        {
            return Convert.ToInt32(dgv.CurrentRow.Cells[0].Value);
        }

        public static DateTime ConvertToDateTime(string selectedItem, DateTime selectedDate)
        {
            DateTime.TryParseExact(selectedItem, "hh:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timePart);
            DateTime selectedSlotTime = selectedDate.Date.Add(timePart.TimeOfDay);
            return selectedSlotTime;
        }

    }
}
