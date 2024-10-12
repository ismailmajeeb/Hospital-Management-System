using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class CreatePatientUserModel : CreateUserModel
    {
        [Display(Name = "Select Doctor Name")]
        Patient
        public List<string> DoctorsName { get; set; }
    }
}
