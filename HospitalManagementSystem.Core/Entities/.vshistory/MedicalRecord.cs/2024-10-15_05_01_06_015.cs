using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public string? Discription { get; set; }
        public string? Diagnosis { get; set; }
        public string? Treatment { get; set; }
       
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public int? AppointmentId { get; set; }
        
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public Appointment? Appointment { get; set; }
    }
}
