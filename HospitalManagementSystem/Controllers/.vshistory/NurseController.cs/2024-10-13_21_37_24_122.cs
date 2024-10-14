


namespace HospitalManagementSystem.Controllers
{
    public class NurseController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public NurseController(IUnitOfWork context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }
        // GET: NurseController
        [Authorize(Roles = $"{SD.Admin}")]
        public async Task<ActionResult> Index()
        {
            var nurses = await _context.Nurses.GetAllAsync();
            return View(nurses);
        }



        [Authorize(Roles = SD.Nurse)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var nurse = await _context.Nurses.FindAsync(n => n.UserId == userId);
            var model = new NurseDashBoardModel
            {
                Name = nurse.Name,
                Age = nurse.Age,
                Gender = nurse.Gender,
            };
            return View(model);
        }



        // GET: NurseController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var nurse = await _context.Nurses.GetByIdAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // GET: NurseController/Create
        [Authorize(Roles = SD.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: NurseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Create(Nurse obj)
        {
            if (ModelState.IsValid)
            {
                await _context.Nurses.AddAsync(obj);
                await _context.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: NurseController/Edit/5
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Edit(int id)
        {
            var nurse = await _context.Nurses.GetByIdAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // POST: NurseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Edit(Nurse obj)
        {
            if (ModelState.IsValid)
            {
                //obj.UpdatedAt = DateTime.Now;
                _context.Nurses.Update(obj);
                await _context.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: NurseController/Delete/5
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Delete(int id)
        {
            var nurse = await _context.Nurses.GetByIdAsync(id);
            if (nurse == null)
            {
                return NotFound();
            }
            return View(nurse);
        }

        // POST: NurseController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> DeletePost(int id)
        {
            var nurse = await _context.Nurses.GetByIdAsync(id);
            _context.Nurses.Delete(nurse);
            await _context.CompleteAsync();
            return RedirectToAction("Index");
        }
    }
}
