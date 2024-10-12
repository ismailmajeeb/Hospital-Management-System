using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientAppointmentModel
    {
        [Display(Name = "Patient Name")]
        public int PatientName { get; set; }
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }

    }
}
