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
            builder.HasMany(d => d.MedicalRecords)
                    .WithOne(m => m.Doctor)
                    .HasForeignKey(m => m.DoctorID)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Patients)
                   .WithMany(m => m.Doctors)
                   .UsingEntity();


        }
    }
}
