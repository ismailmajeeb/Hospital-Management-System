
using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    internal class PatientCongfigration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {

            builder.HasOne(p => p.Doctor)
                   .WithMany(d => d.Patients)
                   .HasForeignKey(a => a.DoctorId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.MedicalRecords)
                   .WithOne(m => m.Patient)
                   .HasForeignKey(a => a.PatientID)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p=>p.Appointments)
                   .WithOne(m => m.Patient)
                   .HasForeignKey(a => a.PatientId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
