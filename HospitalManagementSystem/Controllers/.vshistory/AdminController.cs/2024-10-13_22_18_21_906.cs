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
        public async Task<IActionResult> Dashboard()
        {
            var viewModel = new AdminDashboardModel
            {
                Patients = await unitOfWork.Patients.GetAllAsync(),
                Doctors = await unitOfWork.Doctors.GetAllAsync(),
                Nurses = unitOfWork.Nurses.ToList(),
                Appointments = unitOfWork.Appointments.Include(a => a.Patient).Include(a => a.Doctor).ToList()
            };

            return View(viewModel);
        }
    }
}
