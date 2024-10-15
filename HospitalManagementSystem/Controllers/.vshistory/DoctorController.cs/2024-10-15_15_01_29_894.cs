

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
            var temp = await unitOfWork.Doctors.GetAllAsync();
            IEnumerable<DoctorsIndexModel> model = temp.Select(p => new DoctorsIndexModel { UserId = p.UserId, Name = p.Name });
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = SD.Doctor)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var doctor = await unitOfWork.Doctors.FindAsync(p => p.UserId == userId);

            var Appointments = await unitOfWork.Appointments.FindAllAsync(a => a.DoctorId == doctor.Id, a => a.DateTime, includes: ["Patient"]);
           
            var model = new DoctorDashBoardModel
            {
                Age = doctor.Age,
                Gender = user.Gender,
                Name = doctor.Name,
                Appointments = Appointments.ToList(),
            };
            return View();
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
                Gender = model.Gender,
            };
            await userManager.CreateAsync(user);
            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;
            await unitOfWork.Doctors.AddAsync(new()
            {
                Age = (year != 0) ? DateTime.Now.Year - year : 0,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                
            });
            await userManager.AddToRoleAsync(user, SD.Doctor);
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(string Id = null)
        {
            if (Id == null)
            {
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            var user = await userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");

            var doctor = await unitOfWork.Doctors.FindAsync(d=>d.UserId == Id);
            if (doctor == null) return View("Error");

            var model = new EditDoctorModel
            {
                FirstName = doctor.Name.Split(' ')?.FirstOrDefault(),
                LastName = doctor.Name.Split(' ')?.LastOrDefault(),
                Address = user.Address,
                DateOfBirth = user.DateOfbirth,
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
            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;
            var doctor = new Doctor()
            {
                Name = model.FirstName + " " + model.LastName,
                Age = (year != 0) ? DateTime.Now.Year - year : 0,

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
                Patients = doctor.Patients.ToList(),
                Appointments = doctor.Appointments?.ToList(),
            };

            return View(model);
        }
    }
}
