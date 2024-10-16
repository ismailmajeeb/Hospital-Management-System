


using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Doctors
{
    public class DoctorDashBoardModel
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments { get; set; }
    }
}
