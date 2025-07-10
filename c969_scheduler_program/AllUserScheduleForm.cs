
using c969_scheduler_program.Services;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class AllUserScheduleForm : Form
    {
        public AllUserScheduleForm()
        {
            InitializeComponent();
            var usersSchedule = ReportService.GetUserSchedules();
            schedulesDgv.DataSource = usersSchedule;
            this.Load += AllUserScheduleForm_Load;
        }

        private void AllUserScheduleForm_Load(object sender, EventArgs e)
        {
            FormatDgv();
        }
        private void FormatDgv()
        {
            // Make all cells read-only
            schedulesDgv.ReadOnly = true;

            // Enable column sorting
            foreach (DataGridViewColumn column in schedulesDgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Specific column formatting
            schedulesDgv.RowHeadersVisible = false;
            //schedulesDgv.Columns["MonthDisplay"].HeaderText = "Month";
            //schedulesDgv.Columns["MonthValue"].Visible = false;
            //schedulesDgv.Columns["Count"].HeaderText = "# Appointments";
            //schedulesDgv.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Optional: Zebra striping
            schedulesDgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }
    }
}
