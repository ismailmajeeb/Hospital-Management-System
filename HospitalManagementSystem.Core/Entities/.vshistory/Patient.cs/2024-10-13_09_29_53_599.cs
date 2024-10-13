namespace HospitalManagementSystem.Core.Entities
{
    public class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? DoctorId { get; set; }

        public string? BloodGroup { get; set; }
        public string? ChronicDiseases { get; set; }
        public string? Allergies { get; set; }

        public Doctor? Doctor { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public List<MedicalRecords>? MedicalRecords { get; set; }

    }
    public static class BloodGroup
    {
        public static List<string> BloodGroupsList { get; set; } = new List<string>()
        {
            "O-",
            "O+",
            "A-",
            "A+",
            "B-",
            "B+",
            "AB-",
            "AB+",
        };

    }

}
