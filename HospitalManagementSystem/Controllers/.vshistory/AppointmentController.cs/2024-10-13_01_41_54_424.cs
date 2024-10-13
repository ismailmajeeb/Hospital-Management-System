

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<ApplicationUser> userManager;

        public AppointmentController(IUnitOfWork context,
                                     UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.userManager = userManager;
        }
        // GET: AppointmentController
        public async Task<ActionResult> Index()
        {

            var appointments = await _context.Appointments.GetAllAsync();
            return View(appointments);
        }
        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public IActionResult Create()
        {
            return View();
        }
        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Create(AddAppointmentModel model)
        {
            if (!ModelState.IsValid) return View(model);
            Appointment appointment = new()
            {
                DateTime = model.AppointmentDate,
                Doctor = await _context.Doctors.FindAsync(d => d.User.UserName == model.DoctorUserName),
                Reason = model.Reason,
                Status = Status.Pending,
                Patient = await _context.Patients.FindAsync(d => d.User.UserName == model.PatientUserName)
            };
            await _context.Appointments.AddAsync(appointment);
            await _context.CompleteAsync();
            return RedirectToAction("Index");

        }

        // GET: AppointmentController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: AppointmentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Appointment obj)
        {
            if (ModelState.IsValid)
            {
                //obj.UpdatedAt = DateTime.Now;
                _context.Appointments.Update(obj);
                await _context.CompleteAsync();
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Cancel(int id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(id);
            if (appointment == null) return View("Error");
            appointment.Status = Status.Cancelled;
            _context.Appointments.Update(appointment);
            await _context.CompleteAsync();
            if (User.IsInRole(SD.Admin))
                return RedirectToAction("Index");
            if (User.IsInRole(SD.Patient))
                return RedirectToAction("Appointments", "Patients");
            return View("Error");
        }
    }
}
