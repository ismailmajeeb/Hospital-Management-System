using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientProfileModel:ProfileModel
    {

        public string? BloodGroup { get; set; } = string.Empty;
        public string? Allergies { get; set; } = string.Empty;
        public string? ChronicDiseases { get; set; } = string.Empty;
        public string? CurrentMedications { get; set; } = string.Empty;
    }
}
