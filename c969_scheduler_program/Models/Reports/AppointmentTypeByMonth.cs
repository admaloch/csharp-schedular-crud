using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace c969_scheduler_program.Models.Reports
{
    public class AppointmentTypeByMonth
    {
        // Formatted display month (e.g. "July 2025")
        public string MonthDisplay { get; set; }

        // Underlying DateTime for sorting/calculations (optional)
        private DateTime _monthValue;
        public DateTime MonthValue
        {
            get => _monthValue;
            set
            {
                _monthValue = value;
                MonthDisplay = value.ToString("MMMM yyyy");
            }
        }

        public string Type { get; set; }
        public int Count { get; set; }

        // Parameterless constructor for serialization
        public AppointmentTypeByMonth() { }

        // Convenience constructor
        public AppointmentTypeByMonth(DateTime month, string type, int count)
        {
            MonthValue = month;
            Type = type;
            Count = count;
        }
    }

}
