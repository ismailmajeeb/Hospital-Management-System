using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorsIndexModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public List<Patient>? Patients { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
