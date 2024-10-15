using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Admins
{
    public class CreateAdminModel:UserModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
