using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class Patient:BaseEntity
    {
        int Id;
        public string Name { get; set; }
        public int DoctorId {  get; set; }
        public String BloodGroup { get; set; }  

        public Doctor Doctor { get; set; }
        public List<Appointment> Appointments;
        public List<MedicalRecords> MedicalRecords { get; set; }

    }
}
