using HospitalManagementSystem.DataAccess.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.DataAccess
{
    public interface IUnitOfWork : IDisposable
    {

        INurseRepository Nurses { get; }
        IDoctorRepository Doctors { get; }
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        IMedicalRecordRepository MedicalRecords { get; }

        Task<int> CompleteAsync();
    }
}
