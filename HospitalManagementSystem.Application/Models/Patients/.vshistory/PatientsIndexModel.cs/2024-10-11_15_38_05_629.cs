using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientsIndexModel
    {
        public string Name { get; set; }
        [Display(Name = "Doctor Names")]
        public List<string> DoctorName { get; set; }
        public string Name { get; set; }
        public string Name { get; set; }
    }
}
