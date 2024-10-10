using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Account
{
    public class VerifyAuthenticatorModel
    {

        [Required]
        public string Code { get; set; }
        public string? ReturnUrl { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class VerifyAuthenticatorResponseModel
    {
        public bool IsLockedOut {  get; set; }
        public bool Succeeded {  get; set; }
    }
}
