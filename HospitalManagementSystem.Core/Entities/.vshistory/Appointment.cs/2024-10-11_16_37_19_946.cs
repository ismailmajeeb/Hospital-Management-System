using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status status { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
    }
    public enum Status
    {
        Pending,
        Confirmed,
        Cancelled,

    }
}
