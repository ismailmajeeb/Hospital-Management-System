using HospitalManagementSystem.Core.Entities;
using HospitalManagementSystem.DataAccess.Persistence;
using HospitalManagementSystem.DataAccess.Repositories;
using HospitalManagementSystem.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IPatientRepository Patients{ get; private set; }
        public IDoctorRepository Doctors{ get; private set; }
        public INurseRepository Nurses{ get; private set; }
        public IAppointmentRepository Appointments { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Patients = new PatientRepository(_context);
            Doctors = new DoctorRepository(_context);
            Nurses = new NurseRepository(_context);
            Appointments = new AppointmentRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
