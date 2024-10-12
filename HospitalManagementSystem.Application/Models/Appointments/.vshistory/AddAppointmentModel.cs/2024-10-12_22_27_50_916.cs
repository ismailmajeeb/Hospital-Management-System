using HospitalManagementSystem.Application.Filters;
using HospitalManagementSystem.Core.Entities;

using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class AddAppointmentModel
    {
        public string PatientUserName { get; set; }
        public string DoctorUserName { get; set; }

        [Display(Name = "Appointment Date")]
        [DateOfBirthValdiation]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
    }
}
