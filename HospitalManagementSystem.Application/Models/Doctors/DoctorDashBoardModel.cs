


using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorDashBoardModel
    {
        public int AppointmentId { get; set; }
        public string DoctorName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime? AppointmentDateTime { get; set; }
        public string? AppointmentPatientName { get; set; }
    }
}
