
using c969_scheduler_program.Models.Reports;
using c969_scheduler_program.Services;
using c969_scheduler_program.Utils;
using System;
using System.Drawing;
using System.Windows.Forms;



namespace c969_scheduler_program
{
    public partial class ApptTypesForm : Form
    {
        //private List<AppointmentTypeByMonth>
        public ApptTypesForm()
        {
            InitializeComponent();
            var apptsByMonth = ReportService.GetAppointmentTypesByMonth();
            apptMonthDgv.DataSource = apptsByMonth;
            this.Load += ApptTypesForm_Load;
        }

        private void ApptTypesForm_Load(object sender, EventArgs e)
        {
            FormatDgv();
        }
        private void FormatDgv()
        {

            // Make all cells read-only
            apptMonthDgv.ReadOnly = true;

            // Enable column sorting
            foreach (DataGridViewColumn column in apptMonthDgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            // Specific column formatting
            apptMonthDgv.RowHeadersVisible = false;
            apptMonthDgv.Columns["MonthDisplay"].HeaderText = "Month";
            apptMonthDgv.Columns["MonthValue"].Visible = false;
            apptMonthDgv.Columns["Count"].HeaderText = "# Appointments";
            apptMonthDgv.Columns["Type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            // Optional: Zebra striping
            apptMonthDgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }


    }
}
