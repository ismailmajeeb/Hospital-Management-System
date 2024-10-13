

namespace HospitalManagementSystem.Core.Entities
{
    public class Doctor :BaseEntity
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public ICollection<Appointment>? Appointments {  get; set; }        
        public List<MedicalRecords>? MedicalRecords { get; set; }
    }
}
