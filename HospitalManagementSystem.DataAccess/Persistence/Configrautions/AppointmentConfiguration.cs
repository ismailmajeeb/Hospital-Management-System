using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    internal class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.AppointmentID);
            builder.Property(a => a.AppointmentID).ValueGeneratedOnAdd();

            builder.Property(a => a.PatientID).IsRequired();
            builder.Property(a => a.DoctorID).IsRequired();
            builder.Property(a => a.AppointmentDate).IsRequired();
            builder.Property(a => a.AppointmentTime).IsRequired();
            builder.Property(a => a.Reason).HasMaxLength(500);

            builder.HasOne(a => a.Patient)
                   .WithMany(p => p.Appointments)
                   .HasForeignKey(a => a.PatientID);

            builder.HasOne(a => a.Doctor)
                   .WithMany(d => d.Appointments)
                   .HasForeignKey(a => a.DoctorID);
        }
    }
}
