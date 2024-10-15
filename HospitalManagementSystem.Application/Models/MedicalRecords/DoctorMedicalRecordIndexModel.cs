using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.MedicalRecords
{
    public class DoctorMedicalRecordIndexModel
    {

        public int MedicalRecordId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public string PatientName { get; set; }
    }
}
