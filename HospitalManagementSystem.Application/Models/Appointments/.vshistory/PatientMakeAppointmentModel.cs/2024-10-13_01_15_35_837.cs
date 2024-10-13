using HospitalManagementSystem.Application.Filters;
using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class PatientMakeAppointmentModel
    {
        [AppointmentDateValdiation]
        [Display(Name = "Appointment Date")]
        public DateTime DateTime { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public string Reason { get; set; }
        [Display(Name = "Doctors User Names")]
        public IEnumerable<string> DoctorsUserNames { get; set; } = Enumerable.Empty<string>();

    }
}
