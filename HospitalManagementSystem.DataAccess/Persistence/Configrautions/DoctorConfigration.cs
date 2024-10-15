using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;


namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    public class DoctorConfigration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasMany(d=>d.MedicalRecords).WithOne(m=>m.Doctor)
                   .HasForeignKey(m=>m.DoctorId)
                   .OnDelete(DeleteBehavior.NoAction);
        
            builder.HasMany(d => d.Patients)
                   .WithMany(m => m.Doctors)
                   .UsingEntity<Appointment>(
                               l => l.HasOne(c => c.Patient).WithMany(x => x.Appointments).HasForeignKey(x => x.PatientId).OnDelete(DeleteBehavior.Restrict),
                               r => r.HasOne(c => c.Doctor).WithMany(x => x.Appointments).HasForeignKey(x => x.DoctorId).OnDelete(DeleteBehavior.Cascade)
                   );                 
        }
    }
}
