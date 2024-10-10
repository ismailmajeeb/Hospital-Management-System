

namespace HospitalManagementSystem.Core.Entities
{
    public class Doctor
    {
        public int Id {  get; set; }
        public List<Appointment> Appointments {  get; set; }
    }
}
