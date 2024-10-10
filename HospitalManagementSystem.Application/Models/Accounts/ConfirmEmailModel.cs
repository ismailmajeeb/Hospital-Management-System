using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.User
{
    public class ConfirmEmailModel
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Token { get; set; }  
    }
}
