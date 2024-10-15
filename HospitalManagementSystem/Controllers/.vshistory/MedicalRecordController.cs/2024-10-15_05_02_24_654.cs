using HospitalManagementSystem.Application.Models.MedicalRecords;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    //[Authorize(Roles = SD.Doctor)]
    public class MedicalRecordController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public MedicalRecordController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Create(int Id)
        {
            int appointmentId = Id;
            var appointment = await unitOfWork.Appointments.FindAsync(a => a.Id == appointmentId ,includes: ["Patient","Doctor"]);
            if (appointment == null) return View("Error");
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
            if(doctor == null || patient == null) return View("Error");
            var record = new MedicalRecord()
            {
                Appointment = appointment,
                Doctor = doctor,
                Patient = patient,
                Discription = model.Discription,
                Treatment = model.Treatment,
                Diagnosis = model.Diagnosis,
                AppointmentId = appointment.Id,
                DoctorId = doctor.Id,
                Id = 1,
                PatientId = patient.Id,

            });
            await unitOfWork.MedicalRecords.AddAsync();
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Doctor", "MedicalRecord");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(int Id)
        {
            var record = await unitOfWork.MedicalRecords.FindAsync(m => m.Id == Id);
            if (record == null) return View("Error");
            return View(new EditMedicalRecordModel
            {
                Id = Id,
                AppointmentId = record.AppointmentId,
                Diagnosis = record.Diagnosis,
                Discription = record.Discription,
                Treatment = record.Treatment,

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
           
            return RedirectToAction("Doctor", "MedicalRecord");

        }
    }
}
