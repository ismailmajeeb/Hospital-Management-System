using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class AppointmentDetailsModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
        public bool IsMedicalRecordCreated { get; set; }
        public MedicalRecord? MedicalRecord { get; set; }
    }
}
