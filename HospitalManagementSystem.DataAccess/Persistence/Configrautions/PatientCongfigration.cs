
using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    internal class PatientCongfigration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasMany(d => d.MedicalRecords).WithOne(m => m.Patient)
                   .HasForeignKey(m => m.PatientId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
