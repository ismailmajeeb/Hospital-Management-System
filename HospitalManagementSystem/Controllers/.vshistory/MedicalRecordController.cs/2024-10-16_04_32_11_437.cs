using HospitalManagementSystem.Application.Models.MedicalRecords;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    [Authorize(Roles = SD.Doctor)]
    public class MedicalRecordController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MedicalRecordController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        [HttpGet]
        [Authorize(Roles = $"{SD.Doctor},{SD.Patient}")]
        public async Task<IActionResult> Details(int Id)
        {
            var record = await unitOfWork.MedicalRecords.FindAsync(m => m.Id == Id, includes: ["Doctor", "Appointment", "Patient"]);
            if (record == null) return View("Error");
            return View(new MedicalRecordDetailsModel
            {
                AppointmentDateTime = record.Appointment.DateTime,
                Description = record.Discription,
                Diagnosis = record.Diagnosis,
                DoctorName = record.Doctor.Name,
                PatientName = record.Patient.Name,
                Treatment = record.Treatment,
                MedicalRecordId = record.Id,

            });
        }


        [HttpGet]
        public async Task<IActionResult> Create(int Id)
        {
            int appointmentId = Id;
            var appointment = await unitOfWork.Appointments.FindAsync(a => a.Id == appointmentId, includes: ["Patient", "Doctor"]);
            if (appointment == null || appointment.Status != Status.Confirmed) return View("Error");
            return View(new CreateMedicalRecordModel
            {
                AppointmentId = appointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
                DoctorName = appointment.Doctor.Name,
                PatientName = appointment.Patient.Name,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMedicalRecordModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var appointment = await unitOfWork.Appointments.GetByIdAsync(model.AppointmentId);
            var doctor = await unitOfWork.Doctors.GetByIdAsync(model.DoctorId);
            var patient = await unitOfWork.Patients.GetByIdAsync(model.PatientId);
            if (doctor == null || patient == null) return View("Error");
            var record = new MedicalRecord()
            {
                Appointment = appointment,
                Doctor = doctor,
                Patient = patient,
                Discription = model.Discription,
                Treatment = model.Treatment,
                Diagnosis = model.Diagnosis,
                AppointmentId = appointment.Id,
            };
            await unitOfWork.MedicalRecords.AddAsync(record);
            await unitOfWork.CompleteAsync();
            return RedirectToAction("MedicalRecords", "Doctor");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var record = await unitOfWork.MedicalRecords.FindAsync(m => m.Id == Id, includes: ["Doctor", "Patient"]);
            if (record == null) return View("Error");
            return View(new EditMedicalRecordModel
            {
                Id = Id,
                AppointmentId = record.AppointmentId,
                Diagnosis = record.Diagnosis,
                Discription = record.Discription,
                Treatment = record.Treatment,
                DoctorName = record.Doctor.Name,
                PatientName = record.Patient.Name,
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMedicalRecordModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var record = await unitOfWork.MedicalRecords.GetByIdAsync(model.Id);
            if (record == null) return View("Error");

            record.Diagnosis = model.Diagnosis;
            record.Discription = model.Discription;
            record.Treatment = model.Treatment;
            await unitOfWork.CompleteAsync();

            return RedirectToAction("MedicalRecords","Doctor",new { Id = User.FindFirstValue(ClaimTypes.NameIdentifier)});

        }
    }
}
