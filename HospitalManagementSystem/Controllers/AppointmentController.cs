using HospitalManagementSystem.Core.Entities;
using HospitalManagementSystem.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IUnitOfWork _context;

        public AppointmentController(IUnitOfWork context)
        {
            _context = context;
        }
        // GET: AppointmentController
        public async Task<ActionResult> Index()
        {
            var appointments = await _context.Appointments.GetAllAsync();
            return View(appointments);
        }

        // GET: AppointmentController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // GET: AppointmentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Appointment obj)
        {
            if (ModelState.IsValid)
            {
                await _context.Appointments.AddAsync(obj);
                _context.Complete();
                return RedirectToAction("Index");
            }
            return View(obj);
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
        public async Task<ActionResult> Edit(Appointment obj)
        {
            if (ModelState.IsValid)
            {
                //obj.UpdatedAt = DateTime.Now;
                _context.Appointments.Update(obj);
                _context.Complete();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        // GET: AppointmentController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: AppointmentController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeletePost(int id)
        {
            var appointment = await _context.Appointments.GetByIdAsync(id);
            _context.Appointments.Delete(appointment);
            _context.Complete();
            return RedirectToAction("Index");
        }
    }
}
