

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        [HttpGet]

        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await unitOfWork.Patients.FindAsync(p=>p.UserId == userId);
            var model = new PatientDashBoardModel
            {
                Age = patient.Age,
                Gender = patient.Gender,
                BloodGroup = patient.BloodGroup,
                Name = patient.Name,
                NextAppointmentDateTime = patient.Appointments.FirstOrDefault().DateTime,
                DoctorName = patient.Doctor.Name,
                MedicalRecord = patient.MedicalRecords.Select( m => m.Description ).ToList(),
            };
            return View(new Patient { Appointments = new(),MedicalRecords = new()});
        }
    }
}
