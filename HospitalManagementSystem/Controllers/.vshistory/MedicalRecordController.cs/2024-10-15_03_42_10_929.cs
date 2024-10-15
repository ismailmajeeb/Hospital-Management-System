using HospitalManagementSystem.Application.Models.MedicalRecords;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class MedicalRecordController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MedicalRecordController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = SD.Doctor)]
        public IActionResult Create(int appointmentId)
        {
            unitOfWork.Appointments.FindAsync(a => a.Id == appointmentId, includes: ["Doctor", "Patient"]);
            return View(new CreateMedicalRecordModel
            {
                AppointmentId = appointmentId,
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Doctor)]
        public IActionResult Create(CreateMedicalRecordModel model)
        {
            if (!ModelState.IsValid) return View(model); 

        }
    }
}
