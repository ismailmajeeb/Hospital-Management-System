using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Nurses
{
    public class CreateNurseModel : UserModel
    {
        [Display(Name = "First Name")]
        [OneWord]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [OneWord]
        public string LastName { get; set; }
    }
}
