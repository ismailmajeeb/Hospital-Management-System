using HospitalManagementSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class AddAppointmentModel
    {
        
        public string PatientUserName { get; set; }
        public string DoctorUserName { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
    }
}
