


using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientDashBoardModel
    {
        public string Name {  get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string BloodGroup { get; set; }
        public string ChronicDiseases { get; set; }
        public string Allergies {  get; set; }
        public DateTime? NextAppointmentDateTime { get; set; }
        public string DoctorName { get; set; }
        public List<string> ?MedicalRecordDiscription { get; set; }
    }
}
