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

    public class PatientRepository : GenericRepository<Patient>,IPatientRepository
    {
        private readonly ApplicationDbContext _context;

        public PatientRepository(ApplicationDbContext context) : base(context)
        {
        }
    }

}
