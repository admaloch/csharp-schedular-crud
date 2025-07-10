
using System;
using System.Windows.Forms;
using c969_scheduler_program.Models.Reports;
using c969_scheduler_program.Services;



namespace c969_scheduler_program
{
    public partial class ApptTypesForm : Form
    {
        //private List<AppointmentTypeByMonth>
        public ApptTypesForm()
        {
            InitializeComponent();
            var apptsByMonth = ReportService.GetAppointmentTypesByMonth();
            foreach (var apptType in apptsByMonth)
            {
                Console.WriteLine($"count: {apptType.Count}/n month: {apptType.MonthDisplay} /n type: {apptType.Type}");
            }
            apptMonthDgv.DataSource = apptsByMonth;
        }
    }
}
