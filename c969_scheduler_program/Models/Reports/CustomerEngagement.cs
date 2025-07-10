using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace c969_scheduler_program.Models.Reports
{
    internal class CustomerEngagement
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int TotalAppointments { get; set; }
        public DateTime FirstAppointment { get; set; }
        public DateTime LastAppointment { get; set; }
        public string FavoriteType { get; set; }
    }
}
