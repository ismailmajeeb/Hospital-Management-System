
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Models.User
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        public string Email { get; set; }
      

    }
    public class ForgetPasswordResponseModel
    {


    }
}
