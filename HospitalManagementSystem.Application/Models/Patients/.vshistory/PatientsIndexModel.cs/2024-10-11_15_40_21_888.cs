
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientsIndexModel
    {
        public string Name { get; set; }
        [Display(Name = "Doctor Name")]
        public string DoctorName { get; set; }
    }
}
