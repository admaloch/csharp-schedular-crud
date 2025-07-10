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
        public static object GetUserSchedules()
        {
            // Join with User table to get usernames
            return Appointment.GetAllAppointments()
                .OrderBy(a => a.UserId)
                .ThenBy(a => a.Start)
                .Select(a => new  // Anonymous type for binding
                {
                    User = User.GetById(a.UserId)?.UserName ?? "Unknown",
                    a.UserId,
                    a.CustomerName,
                    a.Title,
                    a.Type,
                    Start = a.Start.ToString("MM/dd/yyyy hh:mm tt"),
                    End = a.End.ToString("MM/dd/yyyy hh:mm tt"),
                    a.Location
                })
                .ToList();
        }
    }
}

