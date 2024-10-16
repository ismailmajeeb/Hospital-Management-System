using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.MedicalRecords
{
    public class MedicalRecordDetailsModel
    {
        public string? Description { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public int MedicalRecordId { get; set; }

        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime AppointmentDateTime { get; set; }
    }
}
