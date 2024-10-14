
using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class EditAppointmentModel
    {
        public int Id { get; set; }
        public int PatientName { get; set; }
        public int DoctorName { get; set; }
        public DateTime DateTime { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }


    }
}
