
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class EditPatientModel : UserModel
    {
        public string BloodGroup { get; set; }
        public string ChronicDiseases { get; set; }
        
        public string Allergies { get; set; }

        [Display(Name = "First Name")]
        [OneWord]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [OneWord]
        public string LastName { get; set; }
    }
}

