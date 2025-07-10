using c969_scheduler_program.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace c969_scheduler_program
{
    public partial class CustomerEngagementForm : Form
    {
        public CustomerEngagementForm()
        {
            InitializeComponent();
            var usersSchedule = ReportService.GetCustomerEngagementReport();
            customerDgv.DataSource = usersSchedule;
            this.Load += AllUserScheduleForm_Load;
        }

        private void AllUserScheduleForm_Load(object sender, EventArgs e)
        {
            FormatDgv();
        }
        private void FormatDgv()
        {
            // Make all cells read-only
            customerDgv.ReadOnly = true;

            // Enable column sorting
            foreach (DataGridViewColumn column in customerDgv.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            customerDgv.RowHeadersVisible = false;
            //customerDgv.Columns["CustomerName"].HeaderText = "Customer";
            customerDgv.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }
    }
}
