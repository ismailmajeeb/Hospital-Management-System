using HospitalManagementSystem.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Services
{
    internal class DoctorService
    {
        private readonly IUnitOfWork unitOfWork;

        public DoctorService(IUnitOfWork _unitOfWork)
        {
            unitOfWork = _unitOfWork;
        }

    }
}
