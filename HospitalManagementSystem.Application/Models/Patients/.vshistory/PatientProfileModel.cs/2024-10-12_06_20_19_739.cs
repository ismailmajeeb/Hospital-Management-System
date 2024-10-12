using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientProfileMoel
    {

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string NationalIdOrPassport { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Address { get; set; }
        public string BloodType { get; set; }
        public string Allergies { get; set; }
        public string ChronicDiseases { get; set; }
        public string CurrentMedications { get; set; }
    }
}
