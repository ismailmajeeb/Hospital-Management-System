using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class AppointmentDetailsModel
    {
        public int AppointmentId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
        public bool IsMedicalRecordCreated { get; set; }
        public int? MedicalRecordId { get; set; }
    }
}
