using HospitalManagementSystem.Application.Models.Admins;
using HospitalManagementSystem.Core.Entities;

namespace HospitalManagementSystem.Controllers
{

    public class AdminController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public AdminController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
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
        public async Task<IActionResult> Profile()
        {
            var user = await userManager.GetUserAsync(User);
            return View(new AdminProfileModel()
            {
                Id = user.Id,
                Address = user.Address,
                DateOfBirth = user.DateOfbirth,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                NationalIdOrPassport = user.SSN,
                PhoneNumber = user.PhoneNumber,
                Name = user.UserName,
                Gender = user.Gender,
            });

        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SD.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAdminModel model)
        {
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
            var result = await userManager.CreateAsync(user, "Patient123");
            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);
            if (!result.Succeeded) return View(model);

            await userManager.AddToRoleAsync(user, SD.Patient);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(string Id = null)
        {
            if (Id == null)
            {
                Id = User.FindFirstValue(ClaimTypes.Name);
            }
            
            var user = await userManager.FindByIdAsync(Id);
            if(user == null) return View("Error");
            var model =new EditAdminModel()
            {
                Id = Id,
                SSN = user.SSN,
                Address = user.Address,
                Email = user.Email,
                DateOfBirth = user.DateOfbirth,
                Gender = user.Gender,
                UserName = user.UserName,
                PhoneNumber =user.PhoneNumber,

                
                
               
            };
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SD.Admin)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAdminModel model)
        {
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
            var result = await userManager.CreateAsync(user, "Patient123");
            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);
            if (!result.Succeeded) return View(model);

            await userManager.AddToRoleAsync(user, SD.Patient);

            return RedirectToAction("Index");
        }
    }
}
