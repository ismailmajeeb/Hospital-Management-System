﻿using HospitalManagementSystem.Core.Entities;
using HospitalManagementSystem.DataAccess.Identity;
using HospitalManagementSystem.DataAccess.Persistence.Configrautions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Emit;

namespace HospitalManagementSystem.DataAccess.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options):base(options) { }
       
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Nurse> Nurses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Appointment> Doctors { get; set; }
        
        public ApplicationDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new NurseConfiguration());
            builder.ApplyConfiguration(new AppointmentConfiguration());
        }
    }


}
