using HospitalManagementSystem.Application.Models.Admins;
using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Controllers
{

    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Dashboard()
        {
            var patients = await unitOfWork.Patients.GetAllAsync();
            var doctors = await unitOfWork.Doctors.GetAllAsync();
            var nurses = await unitOfWork.Nurses.GetAllAsync();
            var appointments = await unitOfWork.Appointments.FindAllAsync(a => true, includes: ["Doctor", "Patient"]);
            var viewModel = new AdminDashboardModel
            {
                PatientsCount = patients.Count(),
                DoctorsCount = doctors.Count(),
                NursesCount = nurses.Count(),
                Appointments = appointments.Take(10),
                AppointmentsCount = appointments.Count(a => a.DateTime >= DateTime.Now)
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public IActionResult Profile()
        {

        }
    }
}
