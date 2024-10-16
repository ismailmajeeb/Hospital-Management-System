using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorProfileModel: ProfileModel
    {
        public string Name { get; set; }
        public List<Patient>? Patients { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
