using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class CreatePatientModel:UserModel
    {
        [Display(Name = "First Name")]
        [OneWord]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [OneWord]
        public string LastName { get; set; }
        public string BloodGroup { get; set; }
        public string ChronicDiseases { get; set; }

        public string Allergies { get; set; }
    }
}
