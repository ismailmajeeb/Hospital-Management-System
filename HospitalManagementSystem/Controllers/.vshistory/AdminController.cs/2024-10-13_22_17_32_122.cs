using HospitalManagementSystem.Application.Models.Admins;

namespace HospitalManagementSystem.Controllers
{
    
    public class AdminController:Controller
    {
        private readonly UnitOfWork unitOfWork;

        public AdminController(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Dashboard()
        {
            var viewModel = new AdminDashboardModel
            {
                Patients = unitOfWork.Patients.ToList(),
                Doctors = unitOfWork.Doctors.ToList(),
                Nurses = unitOfWork.Nurses.ToList(),
                Appointments = unitOfWork.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList()
            };

            return View(viewModel);
        }
    }
}
