
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class CreateDoctorModel:UserModel
    {

        [Display(Name = "First Name")]
        [OneWord]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [OneWord]
        public string LastName { get; set; }

    }
}
