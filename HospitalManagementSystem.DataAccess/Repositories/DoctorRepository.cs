using HospitalManagementSystem.Core.Entities;
using HospitalManagementSystem.DataAccess.Persistence;
using HospitalManagementSystem.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.DataAccess.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>,IDoctorRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
