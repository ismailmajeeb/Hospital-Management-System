using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? SSN { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfbirth { get; set; }

    }
}
