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
        public string Description { set; get; }
        public int PatientID { set; get; }
        public int DoctorID { set; get; }
    }
}
