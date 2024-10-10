

namespace HospitalManagementSystem.Controllers
{
    public class NurseController : Controller
    {
        private readonly IUnitOfWork _context;

        public NurseController(IUnitOfWork context)
        {
            _context = context;
        }
        // GET: NurseController
        public async Task<ActionResult> Index()
        {
            var nurses = await _context.Nurses.GetAllAsync();
            return View(nurses);
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: NurseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Nurse obj)
        {
            if (ModelState.IsValid)
            {
                await _context.Nurses.AddAsync(obj);
                _context.Complete();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: NurseController/Edit/5
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
        public async Task<ActionResult> Edit(Nurse obj)
        {
            if (ModelState.IsValid)
            {
                //obj.UpdatedAt = DateTime.Now;
                _context.Nurses.Update(obj);
                _context.Complete();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: NurseController/Delete/5
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
        public async Task<ActionResult> DeletePost(int id)
        {
            var nurse = await _context.Nurses.GetByIdAsync(id);
            _context.Nurses.Delete(nurse);
            _context.Complete();
            return RedirectToAction("Index");
        }
    }
}
