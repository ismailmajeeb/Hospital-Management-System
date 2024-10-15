using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.MedicalRecords
{
    public class PatientMedicalRecordIndexModel
    {
        public string MedicalRecordId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string DoctorName { get; set; }
    }
}
