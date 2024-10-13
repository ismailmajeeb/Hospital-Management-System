
namespace HospitalManagementSystem.Application.Models.Patients
{
    public class EditPatientModel : UserModel
    {
        public string? BloodGroup { get; set; }
        public string? ChronicDiseases { get; set; }
        public string? Allergies { get; set; }
        
    }
}

