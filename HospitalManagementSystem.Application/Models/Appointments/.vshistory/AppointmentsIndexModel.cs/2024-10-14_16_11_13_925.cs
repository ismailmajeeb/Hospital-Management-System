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
    public class AppointmentsIndexModel
    {
        public int Id {  get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public Status Status { get; set; }
    }
}
