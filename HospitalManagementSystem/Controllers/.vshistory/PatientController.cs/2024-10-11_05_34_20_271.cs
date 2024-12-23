﻿



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
        [Authorize(Roles = SD.Patient)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == userId);
            var model = new PatientDashBoardModel
            {
                Age = patient.Da,
                Gender = patient.Gender,
                BloodGroup = patient.MedicalRecords?.FirstOrDefault()?.BloodGroup,
                Name = patient.Name,
                NextAppointmentDateTime = patient.Appointments?.FirstOrDefault().DateTime,
                DoctorName = patient.Doctor?.Name,   
                MedicalRecordDiscription = patient.MedicalRecords?.Select(m => m.Description).ToList(),
                Allergies = patient.MedicalRecords?.FirstOrDefault()?.Allergies,
                ChronicConditions = patient.MedicalRecords?.FirstOrDefault()?.ChronicConditions,
            };
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Create(CreatePatientModel model)
        {
            if (!ModelState.IsValid) return View("Error"); 

            var user =new ApplicationUser()
            {
                Address = model.Address,
                Email = model.Email,
                
            }
            
            
            return View(model);


        }

    }
}
