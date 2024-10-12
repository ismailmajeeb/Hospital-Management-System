using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class CreateAppointementModel
    {
        [Display(Name = "Patient User Name")]
        public IEnumerable<string> PatientUserName { get; set; }
        [Display(Name = "Doctor User Name")]
        public IEnumerable<string> DoctortUserName { get; set; }
        public IEnumerable<string> DoctortUserName { get; set; }
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
    }
}
