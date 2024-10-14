using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class CreatePatientModel:UserModel
    {
        public string BloodGroup { get; set; }
        public string ChronicDiseases { get; set; }

        public string Allergies { get; set; }
    }
}
