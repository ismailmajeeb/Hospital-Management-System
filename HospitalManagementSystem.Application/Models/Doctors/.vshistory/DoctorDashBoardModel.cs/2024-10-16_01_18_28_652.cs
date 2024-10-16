


using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorDashBoardModel
    {
        public string DoctorName { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public DateTime AppointmentDataTime { get; set; }
        public string AppointmentPatientDateTime { get; set; }
    }
}
