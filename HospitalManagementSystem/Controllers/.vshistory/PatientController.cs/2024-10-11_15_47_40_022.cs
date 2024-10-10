




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
        [Authorize(Roles = $"{SD.Admin},{SD.SuperAdmin}")]
        public async Task<IActionResult> Index()
        {
            var temp = await unitOfWork.Patients.FindAllAsync(p => true, includes: ["Doctor"]);


            temp.Select(p => new PatientsIndexModel { Id = p.Id, Name = p.Name, DoctorName = p.Doctor.Name });
            List<PatientsIndexModel> model = temp.Select(p => new PatientsIndexModel { Name = p.Name, DoctorName = p.Doctor.Name }).ToList();
            return View(model);

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
                Age = patient.Age,
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
        [Authorize(Roles = SD.Admin)]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Create(CreateUserModel model)
        {
            if (!ModelState.IsValid) return View("Error");

            var user = new ApplicationUser()
            {
                Address = model.Address,
                Email = model.Email,
                DateOfbirth = model.DateOfBirth,
                UserName = model.UserName,
                SSN = model.SSN,
                PhoneNumber = model.PhoneNumber,

            };
            var result = await userManager.CreateAsync(user);
            if (!result.Succeeded) return View("Error");

            await unitOfWork.Patients.AddAsync(new()
            {
                Age = (model.DateOfBirth - DateTime.Now).Days / 356,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                Gender = model.Gender,
            });

            await unitOfWork.CompleteAsync();
            return View("Index");

        }





    }
}
