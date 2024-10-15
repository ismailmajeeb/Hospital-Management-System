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
        public string? Discription { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
      
        public string PatientName{ get; set; }
        public string DoctorName { get; set; }
        public int AppointmentDateTime { get; set; }
    }
}
