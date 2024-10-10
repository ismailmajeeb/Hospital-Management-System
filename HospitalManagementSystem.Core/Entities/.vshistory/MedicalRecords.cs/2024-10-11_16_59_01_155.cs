using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Core.Entities
{
    public class MedicalRecords
    {
        public int Id {  get; set; }
        public string? Description { set; get; }
        public string? BloodGroup { get; set; }
        public string? ChronicConditions { get; set; }
        public string? Allergies { get; set; }
        public int PatientID { set; get; }
        public int DoctorID { set; get; }


        public Patient Patient { set; get; }
        public Doctor? Doctor  { set; get; }
    }
}
