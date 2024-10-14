using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Admins
{
    public class AdminDashboardModel
    {
        public int PatientCount { get; set; }
        public int DoctorCount { get; set; }
        public int NurseCount { get; set; }
        public IEnumerable<Nurse> Nurses { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
