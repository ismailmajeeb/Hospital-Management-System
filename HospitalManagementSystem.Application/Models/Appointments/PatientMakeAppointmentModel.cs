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

        [AppointmentDate]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        public string Reason { get; set; }

        public string DoctorUserName { get; set; }

    }
}
