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
    internal class MedicalRecordRepository: GenericRepository<MedicalRecord>,IMedicalRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicalRecordRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
