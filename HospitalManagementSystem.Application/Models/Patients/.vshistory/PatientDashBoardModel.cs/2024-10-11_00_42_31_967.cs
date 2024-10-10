


using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Patient
{
    public class PatientDashBoardModel
    {
        public string Name {  get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime NextAppointmentDateTime { get; set; }
        public string DoctorName { get; set; }
        public List<string> ?MedicalRecord { get; set; }
    }
}
