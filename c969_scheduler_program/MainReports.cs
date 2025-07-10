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
    public partial class MainReports : Form
    {
        public MainReports()
        {
            InitializeComponent();
        }

        private void apptTypesBtn_Click(object sender, EventArgs e)
        {
            ApptTypesForm frm = new ApptTypesForm();
            frm.Show();
        }
    }
}
