

namespace HospitalManagementSystem.Core.Entities
{
    public class Doctor :BaseEntity
    {
        public int Id {  get; set; }
        public string Name { get; set; }

        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Appointment>? Appointments {  get; set; }
        public ICollection<MedicalRecord>? Appointments {  get; set; }
    }
}
