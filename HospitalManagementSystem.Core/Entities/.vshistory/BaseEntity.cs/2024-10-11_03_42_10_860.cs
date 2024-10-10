using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Gender Gender { get; set; }
    }
    public enum Gender
    {
        Male,
        Female
        
    }
}
