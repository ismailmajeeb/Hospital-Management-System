using HospitalManagementSystem.Application.Filters;
using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models
{
    public class UserModel
    {
        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SSN { get; set; }
        [DateOfBirthValdiation]
        public DateTime DateOfBirth { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public Gender Gender { get; set; }

    }
}
