using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class CreatePatientUserModel : CreateUserModel
    {
        public List<string> DoctorsName { get; set; }
    }
}
