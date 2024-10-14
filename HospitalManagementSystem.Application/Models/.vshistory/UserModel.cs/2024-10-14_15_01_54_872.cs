using HospitalManagementSystem.Application.Filters;
using HospitalManagementSystem.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Application.Models
{
    public class UserModel
    {
        public string? Id { get; set; }
        
        [Display(Name = "First Name")]
        [OneWord]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [OneWord]
        public string LastName { get; set; }
        
        public string Address { get; set; }
        
        [Display(Name = "National Id Or Passpart")]
        [OneWord]
        public string SSN { get; set; }
      
        [DateOfBirth]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Gender Gender { get; set; }

    }
}
