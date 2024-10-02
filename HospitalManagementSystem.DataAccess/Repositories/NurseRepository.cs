using HospitalManagementSystem.Core.Entities;
using HospitalManagementSystem.DataAccess.Persistence;
using HospitalManagementSystem.DataAccess.Repositories.IRepositories;


namespace HospitalManagementSystem.DataAccess.Repositories
{
    public class NurseRepository:GenericRepository<Nurse>,INurseRepository
    {
        private readonly ApplicationDbContext _context;

        public NurseRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
