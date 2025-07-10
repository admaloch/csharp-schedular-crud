using c969_scheduler_program.Models;
using c969_scheduler_program.Models.Reports;
using System;
using System.Collections.Generic;
using System.Linq;



namespace c969_scheduler_program.Services
{
    public class ReportService
    {

        // In ReportService.cs
        public static List<AppointmentTypeByMonth> GetAppointmentTypesByMonth()
        {
            return Appointment.GetAllAppointments()
                .GroupBy(a => new
                {
                    Month = a.Start.ToString("MMMM yyyy"), // Format for display
                    a.Type
                })
                .Select(g => new AppointmentTypeByMonth
                {
                    MonthDisplay = g.Key.Month,
                    Type = g.Key.Type,
                    Count = g.Count()
                })
                .OrderBy(x => DateTime.Parse(x.MonthDisplay)) // Chronological sort
                .ToList();
        }
    }
}

