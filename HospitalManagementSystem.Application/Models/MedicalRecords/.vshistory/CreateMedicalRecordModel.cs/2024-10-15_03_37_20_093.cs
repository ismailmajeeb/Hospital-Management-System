using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.MedicalRecords
{
    public class CreateMedicalRecordModel
    {
        public string? Discription { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
        public int? AppointmentId { get; set; }
    }
}
