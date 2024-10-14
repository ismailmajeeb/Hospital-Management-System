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
                Nurses = await unitOfWork.Nurses.GetAllAsync(),
                Appointments = await unitOfWork.Appointments.FindAllAsync(a => true, includes: ["Doctor", "Patient"]),
            };

            return View(viewModel);
        }
    }
}
