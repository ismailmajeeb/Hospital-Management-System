using HospitalManagementSystem.Application.Filters;
using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class PatientMakeAppointmentModel
    {
        [AppointmentDateValdiation]
        public DateTime DateTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public string Reason { get; set; }
    }
}
