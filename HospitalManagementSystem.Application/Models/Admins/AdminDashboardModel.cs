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
        public int PatientsCount { get; set; }
        public int DoctorsCount { get; set; }
        public int NursesCount { get; set; }
        public int AppointmentsCount { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
