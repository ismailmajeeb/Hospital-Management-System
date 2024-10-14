

namespace HospitalManagementSystem.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public DoctorController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.Admin}")]
        public async Task<IActionResult> Index()
        {
            var doctors = await unitOfWork.Doctors.GetAllAsync();
            var model = doctors.Select(d => new DoctorsIndexModel
            {
                Id = d.Id,
                Name = d.Name
            }).ToList();
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
        public async Task<IActionResult> Create(CreateDoctorModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                DateOfbirth = model.DateOfBirth,
                Address = model.Address,
                SSN = model.SSN,
            };
            await userManager.CreateAsync(user);
            await unitOfWork.Doctors.AddAsync(new()
            {
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                Gender = model.Gender,
            });
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(int Id)
        {
            var doctor = await unitOfWork.Doctors.GetByIdAsync(Id);
            var user = await userManager.FindByIdAsync(doctor.UserId);
            if (doctor == null) return View("Error");

            var model = new EditDoctorModel
            {
                FirstName = doctor.Name.TrimStart(' '),
                LastName = doctor.Name.TrimEnd(' '),
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
        public async Task<IActionResult> Edit(EditDoctorModel model)
        {
            if (!ModelState.IsValid) return View("Error");
            var doctor = new Doctor()
            {
                Name = model.FirstName + " " + model.LastName,
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Gender = model.Gender,
            };
            if (doctor == null) return View("Error");
            unitOfWork.Doctors.Update(doctor);
            await unitOfWork.CompleteAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");
            await userManager.DeleteAsync(user);

            var temp = await unitOfWork.Doctors.GetAllAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Profile(int Id)
        {
            var doctor = await unitOfWork.Doctors.GetByIdAsync(Id);
            if (doctor == null) return View("Error");

            var model = new DoctorProfileModel
            {
                Name = doctor.Name,
                Patients = doctor.Patients,
                Appointments = doctor.Appointments,
                MedicalRecords = doctor.MedicalRecords
            };

            return View(model);
        }
    }
}
