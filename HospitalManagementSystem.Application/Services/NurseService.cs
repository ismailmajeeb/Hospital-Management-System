using HospitalManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Services
{
    internal class NurseService
    {
        private readonly IUnitOfWork unitOfWork;

        public NurseService(IUnitOfWork _unitOfWork) 
        {
            unitOfWork = _unitOfWork;
        }
    }
}
