using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class CreatePatientModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        public DateTime DataOfBirth { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

    }
}
