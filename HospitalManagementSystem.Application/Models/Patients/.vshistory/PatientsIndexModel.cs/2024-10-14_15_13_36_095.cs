
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Models.Patients
{
    public class PatientsIndexModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        [OneWord]
        public string UserNane { get; set; }

    }
}
