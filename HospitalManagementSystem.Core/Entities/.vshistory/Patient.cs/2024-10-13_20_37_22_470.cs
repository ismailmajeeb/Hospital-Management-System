namespace HospitalManagementSystem.Core.Entities
{
    public class Patient : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string? BloodGroup { get; set; }
        public string? ChronicDiseases { get; set; }
        public string? Allergies { get; set; }

        public List<Appointment>? Appointments { get; set; }
        public List<Doctor> { set; get; }
        public List<MedicalRecords>? MedicalRecords { get; set; }
        

    }
    public static class BloodGroup
    {
        public static List<string> BloodGroupsList { get;} = new List<string>()
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
