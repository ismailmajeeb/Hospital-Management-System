using HospitalManagementSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.DataAccess.Persistence.Configrautions
{
    internal class NurseConfiguration : IEntityTypeConfiguration<Nurse>
    {
        public void Configure(EntityTypeBuilder<Nurse> builder)
        {
            builder.HasKey(n => n.NurseID);
builder.Property(n => n.NurseID).ValueGeneratedOnAdd();

builder.Property(n => n.UserID)
       .IsRequired();

builder.Property(n => n.Name)
        .IsRequired();

builder.Property(n => n.Department)
       .IsRequired()
       .HasMaxLength(100);

builder.Property(n => n.CreatedAt)
       .IsRequired();

builder.Property(n => n.UpdatedAt)
       .IsRequired();
        }
    }
}
