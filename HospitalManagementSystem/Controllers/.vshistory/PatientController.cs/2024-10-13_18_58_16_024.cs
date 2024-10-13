

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
            var temp = await unitOfWork.Patients.GetAllAsync();
            List<PatientsIndexModel> model = temp.Select(p => new PatientsIndexModel { UserId = p.UserId, Name = p.Name }).ToList();
            return View(model);

        }

        [HttpGet]
        [Authorize(Roles = SD.Patient)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == userId);

            var Appointments = await unitOfWork.Appointments.FindAllAsync(a => a.PatientId == patient.Id, a => a.DateTime, includes: ["Doctor"]);
            var nextAppointment = Appointments.FirstOrDefault();
            var model = new PatientDashBoardModel
            {
                Age = patient.Age,
                Gender = patient.Gender,
                BloodGroup = patient.BloodGroup,
                Name = patient.Name,
                NextAppointmentDateTime = nextAppointment?.DateTime,
                DoctorName = nextAppointment?.Doctor.Name,
                Allergies = patient.Allergies,
                ChronicDiseases = patient.ChronicDiseases,
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
        public async Task<IActionResult> Create(CreatePatientModel model)
        {
            if (!ModelState.IsValid) return View(model);

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
            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);
            if (!result.Succeeded) return View(model);

            await unitOfWork.Patients.AddAsync(new()
            {
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                Gender = model.Gender,
            });

            await unitOfWork.CompleteAsync();

            var temp = await unitOfWork.Patients.GetAllAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");
            await userManager.DeleteAsync(user);

            var temp = await unitOfWork.Patients.GetAllAsync();
            return RedirectToAction("Index");
        }



        [HttpGet]

        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Edit(int Id)
        {
            var patient = await unitOfWork.Patients.GetByIdAsync(Id);
            var user = await userManager.FindByIdAsync(patient.UserId);
            if (user == null || patient == null) return View("Error");
            var model = new EditPatientModel
            {
                Gender = patient.Gender,
                FirstName = patient.Name.TrimStart(' '),
                LastName = patient.Name.TrimEnd(' '),
                Address = user.Address,
                DateOfBirth = user.DateOfbirth.Value,
                Email = user.Email,
                SSN = user.SSN,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(EditPatientModel model)
        {
            if (!ModelState.IsValid) return View("Error");
            var patient = new Patient()
            {
                Name = model.FirstName + " " + model.LastName,
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Gender = model.Gender,
            };
            if (patient == null) return View("Error");
            unitOfWork.Patients.Update(patient);
            await unitOfWork.CompleteAsync();
            return View();
        }

        
        
        
        
        [HttpGet]
        public async Task<IActionResult> Profile()
        {

            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await userManager.GetUserAsync(User);
            if(user == null || patient == null) return View("Error");
            return View(new PatientProfileModel()
            {
                Name = patient.Name,
                Address = user.Address,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                NationalIdOrPassport = user.SSN,
                PhoneNumber = user.PhoneNumber,
                Gender = patient.Gender,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                DateOfBirth = user.DateOfbirth,
                Allergies = patient.Allergies,
                BloodGroup = patient.BloodGroup,
                ChronicDiseases = patient.ChronicDiseases,
            });
        }




        [HttpGet]
        public async Task<IActionResult> Appointments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await unitOfWork.Patients.FindAsync(x => x.UserId == userId);
            if (patient == null) return View("Error");
            var temp = await unitOfWork.Appointments.FindAllAsync(a => a.PatientId == patient.Id, ["Doctor"]);
            if (temp == null) return View(new List<PatientAppointmentsModel>());
            IEnumerable<PatientAppointmentsModel> model = temp.Select(a => new PatientAppointmentsModel
            {
                AppointmentId = a.Id,
                DateTime = a.DateTime,
                Reason = a.Reason,
                Status = a.Status,
                DoctorName = a.Doctor?.Name,
            });
            return View(model);

        }




        /// <summary>
        /// Patient makeing Apointment
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult MakeAppointment()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Patient)]
        public async Task<IActionResult> MakeAppointment(PatientMakeAppointmentModel model)
        {
            if (!ModelState.IsValid) return View(model);

            Appointment appointment = new()
            {
                DateTime = model.AppointmentDate,
                Reason = model.Reason,
                Doctor = await unitOfWork.Doctors.FindAsync(d => d.User.UserName == model.DoctorUserName),
                Status = Status.Pending,
                Patient = await unitOfWork.Patients.FindAsync(p => p.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)),
            };
            
            await unitOfWork.Appointments.AddAsync(appointment);
            await unitOfWork.CompleteAsync();

            return RedirectToAction("Dashboard");
        }
    }
}