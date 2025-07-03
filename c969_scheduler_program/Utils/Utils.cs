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

    }
}
