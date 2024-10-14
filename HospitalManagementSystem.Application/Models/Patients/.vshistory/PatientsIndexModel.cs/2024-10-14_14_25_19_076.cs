
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientsIndexModel
    {
        public int Id { get; set; }    
        public string UserId { get; set; }
        public string Name { get; set; }

    }
}
