using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using c969_scheduler_program.Models.Reports;
using System.Windows.Forms;

namespace c969_scheduler_program.Utils
{
    internal class DgvFormatUtils
    {
        public static void ConfigureExcelFeatures(DataGridView dgv)
        {
            // Enable sorting by clicking headers
            dgv.ColumnHeaderMouseClick += (s, e) =>
            {
                var column = dgv.Columns[e.ColumnIndex];
                string propertyName = column.DataPropertyName;

                if (dgv.DataSource is List<AppointmentTypeByMonth> data)
                {
                    var sorted = column.HeaderCell.SortGlyphDirection == SortOrder.Ascending
                        ? data.OrderByDescending(x => GetPropertyValue(x, propertyName)).ToList()
                        : data.OrderBy(x => GetPropertyValue(x, propertyName)).ToList();

                    dgv.DataSource = sorted;
                    column.HeaderCell.SortGlyphDirection =
                        column.HeaderCell.SortGlyphDirection == SortOrder.Ascending
                            ? SortOrder.Descending
                            : SortOrder.Ascending;
                }
            };

            // Enable right-click context menu
            var menu = new ContextMenuStrip();
            menu.Items.Add("Copy", null, (s, e) => CopySelectedCells(dgv));
            dgv.ContextMenuStrip = menu;
        }

        public static object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj);
        }

        public static void CopySelectedCells(DataGridView dgv)
        {
            StringBuilder sb = new StringBuilder();

            foreach (DataGridViewCell cell in dgv.SelectedCells)
            {
                if (cell.Value != null)
                {
                    sb.Append(cell.Value.ToString()).Append("\t");
                }
            }

            Clipboard.SetText(sb.ToString());
        }
    }
}
