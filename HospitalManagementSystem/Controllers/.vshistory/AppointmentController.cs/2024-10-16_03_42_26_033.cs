

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
        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<ActionResult> Index()
        {

            var appointments = await _context.Appointments.FindAllAsync(a => DateTime.Now < a.DateTime, 
                                                                        a => a.DateTime, includes: ["Doctor", "Patient"]);
            var model = appointments.Select(a => new AppointmentsIndexModel
            {

                AppointmentDate = a.DateTime,
                DoctorName = a.Doctor.Name,
                PatientName = a.Patient.Name,
                Status = a.Status,
                Id = a.Id,
            });
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


        [HttpGet]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(int Id)
        {
            var appointment = await _context.Appointments.FindAsync(a => a.Id == Id && DateTime.Now < a.DateTime, ["Doctor", "Patient"]);
            if (null == appointment) return View("Error");
            return View(new EditAppointmentModel
            {
                DateTime = appointment.DateTime,
                Id = Id,
                DoctorName = appointment.Doctor.Name,
                PatientName = appointment.Patient.Name,
                Reason = appointment.Reason,
                Status = appointment.Status,
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(EditAppointmentModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var appointment = await _context.Appointments.GetByIdAsync(model.Id);
            if (appointment == null) return View("Error");
            appointment.DateTime = model.DateTime;
            appointment.Status = model.Status;
            appointment.Reason = model.Reason;
            _context.Appointments.Update(appointment);
            await _context.CompleteAsync();
            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.Admin},{SD.Patient}")]
        public async Task<IActionResult> Cancel(int Id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(Id);
            if (appointment == null) return View("Error");
            appointment.Status = Status.Cancelled;
            _context.Appointments.Update(appointment);
            await _context.CompleteAsync();
            if (User.IsInRole(SD.Admin))
                return RedirectToAction("Index");
            if (User.IsInRole(SD.Patient))
                return RedirectToAction("Appointments", "Patient");
            return View("Error");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.Admin}")]
        public async Task<IActionResult> Confirm(int Id)
        {
            var appointment = await _context.Appointments.FindAsync(a => Id == a.Id && DateTime.Now < a.DateTime);
            if (appointment == null) return View("Error");
            appointment.Status = Status.Confirmed;
            _context.Appointments.Update(appointment);
            await _context.CompleteAsync();
            return RedirectToAction("Index");

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = $"{SD.Admin},{SD.Doctor}")]
        public async Task<IActionResult> Details(int id)
    }
}
