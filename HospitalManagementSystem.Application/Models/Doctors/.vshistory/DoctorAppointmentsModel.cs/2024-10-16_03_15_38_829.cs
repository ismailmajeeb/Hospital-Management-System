using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorAppointmentModel
    {
        [Required]
        public int AppointmentId { get; set; }
        [Display(Name = "Doctor Name")]
        public string PatientName { get; set; }
        [Display(Name = "Appointment Date")]
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
    }
}
