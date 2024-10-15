

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
            IEnumerable<PatientsIndexModel> model = temp.Select(p => new PatientsIndexModel { UserId = p.UserId, Name = p.Name });
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
                Gender = user.Gender,
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
                Gender = model.Gender,
                

            };
            var result = await userManager.CreateAsync(user);
            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);
            if (!result.Succeeded) return View(model);

            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;

            await unitOfWork.Patients.AddAsync(new()
            {
                Age = year != 0 ? DateTime.Now.Year - year : 0,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                Allergies = model.Allergies,
                BloodGroup = model.BloodGroup,
                ChronicDiseases = model.ChronicDiseases,
            });

            await unitOfWork.CompleteAsync();

            var temp = await unitOfWork.Patients.GetAllAsync();
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {


            var user = await unitOfWork.Patients.FindAsync(p => p.Id == Id);
            if (user == null) return View("Error");
            unitOfWork.Patients.Delete(user);
            await unitOfWork.CompleteAsync();
            var temp = await unitOfWork.Patients.GetAllAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Edit(string Id = null)
        {
            if (Id == null)
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
        
            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == Id);
            if (patient == null) return View("Error");
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");
            var model = new EditPatientModel
            {
                Id = Id,
                Gender = user.Gender,
                FirstName = patient.Name.Split().FirstOrDefault(),
                LastName = patient.Name.Split().LastOrDefault(),
                Address = user.Address,
                DateOfBirth = user.DateOfbirth,
                Email = user.Email,
                SSN = user.SSN,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Allergies = patient.Allergies,
                BloodGroup = patient.BloodGroup,
                ChronicDiseases = patient.ChronicDiseases
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Edit(EditPatientModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = await userManager.FindByIdAsync(model.Id);
            if (user == null) return View("Error");
            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == user.Id);
            if (patient == null) return View("Error");
            patient.Name = model.FirstName + " " + model.LastName;
            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;

            patient.Age = (year != 0) ? DateTime.Now.Year - year : 0;
            patient.Allergies = model.Allergies;
            patient.ChronicDiseases = model.ChronicDiseases;
            patient.BloodGroup = model.BloodGroup;
            
            

            if (patient == null) return View("Error");
            unitOfWork.Patients.Update(patient);
            user.UserName = model.UserName;
            model.FirstName = model.FirstName;
            model.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.Email = model.Email;
            user.NormalizedEmail = model.Email.ToUpper();
            user.NormalizedUserName = model.UserName.ToUpper();
            user.Address = model.Address;
            user.Gender = model.Gender;
            user.DateOfbirth = model.DateOfBirth;
            user.SSN = model.SSN;
            user.Gender = model.Gender;

            await userManager.UpdateAsync(user);

            await unitOfWork.CompleteAsync();
            if (User.IsInRole(SD.Patient))
                return RedirectToAction("Profile", "Patient");
            return RedirectToAction("Index","Patient");
        }

        [HttpGet]
        [Authorize(Roles = SD.Patient)]
        public async Task<IActionResult> Profile()
        {

            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await userManager.GetUserAsync(User);
            if (user == null || patient == null) return View("Error");
            return View(new PatientProfileModel()
            {
                Id = patient.Id,
                Name = patient.Name,
                Address = user.Address,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                NationalIdOrPassport = user.SSN,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                DateOfBirth = user.DateOfbirth,
                Allergies = patient.Allergies,
                BloodGroup = patient.BloodGroup,
                ChronicDiseases = patient.ChronicDiseases,
            });
        }


        [HttpGet]
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Appointments(string Id = null)
        {
            if (Id == null)
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var patient = await unitOfWork.Patients.FindAsync(x => x.UserId == Id);
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
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
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