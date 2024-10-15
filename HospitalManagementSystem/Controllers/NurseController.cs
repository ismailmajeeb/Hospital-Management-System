

using HospitalManagementSystem.DataAccess;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Controllers
{
    public class NurseController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public NurseController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.Admin}")]
        public async Task<IActionResult> Index()
        {
            var temp = await _unitOfWork.Nurses.GetAllAsync();
            IEnumerable<NursesIndexModel> model = temp.Select(p => new NursesIndexModel { UserId = p.UserId, Name = p.Name });
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = SD.Nurse)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = await _unitOfWork.Nurses.FindAsync(p => p.UserId == userId);

            var model = new NurseDashBoardModel
            {
                Age = nurse.Age,
                Gender = user.Gender,
                Name = nurse.Name,
                // Assuming you have similar properties to populate for the nurse
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
        public async Task<IActionResult> Create(CreateNurseModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                SSN = model.SSN,
                Gender = model.Gender,
            };
            await _userManager.CreateAsync(user,"Nurse123");
            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;
            await _unitOfWork.Nurses.AddAsync(new Nurse
            {
                Age = (year != 0) ? DateTime.Now.Year - year : 0,
                Name = model.FirstName + " " + model.LastName,
                User = user,
            });
            await _userManager.AddToRoleAsync(user, SD.Nurse);
            await _unitOfWork.CompleteAsync();
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
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");

            var nurse = await _unitOfWork.Nurses.FindAsync(d => d.UserId == Id);
            if (nurse == null) return View("Error");

            var model = new EditNurseModel
            {
                FirstName = nurse.Name.Split(' ')?.FirstOrDefault(),
                LastName = nurse.Name.Split(' ')?.LastOrDefault(),
                Address = user.Address,
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
        public async Task<IActionResult> Edit(EditNurseModel model)
        {
            if (!ModelState.IsValid) return View("Error");
            var year = (model.DateOfBirth.HasValue) ? model.DateOfBirth.Value.Year : default;
            var nurse = new Nurse()
            {
                Name = model.FirstName + " " + model.LastName,
                Age = (year != 0) ? DateTime.Now.Year - year : 0,
            };
            if (nurse == null) return View("Error");
            _unitOfWork.Nurses.Update(nurse);
            await _unitOfWork.CompleteAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Delete(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user == null) return View("Error");
            await _userManager.DeleteAsync(user);

            var temp = await _unitOfWork.Nurses.GetAllAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.Nurse},{SD.Admin}")]
        public async Task<IActionResult> Profile()
        {
            var nurse = await _unitOfWork.Nurses.FindAsync(p => p.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _userManager.GetUserAsync(User);
            if (user == null || nurse == null) return View("Error");
            return View(new NurseProfileModel()
            {
                Name = nurse.Name,
                Address = user.Address,
                Email = user.Email,
                IsEmailConfirmed = user.EmailConfirmed,
                NationalIdOrPassport = user.SSN,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                IsTwoFactorEnabled = user.TwoFactorEnabled,
                DateOfBirth = user.DateOfbirth,
            });
        }
    
        
    }
}
