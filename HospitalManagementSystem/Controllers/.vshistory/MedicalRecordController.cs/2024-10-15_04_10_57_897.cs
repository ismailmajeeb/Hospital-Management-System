﻿using HospitalManagementSystem.Application.Models.MedicalRecords;
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

        [HttpPost]
        public async Task<IActionResult> Create(int appointmentId)
        {
            var appointment = await unitOfWork.Appointments.FindAsync(a => a.Id == appointmentId, includes: ["Doctor"]);
            return View(new CreateMedicalRecordModel
            {
                AppointmentId = appointmentId,
                DoctorId = appointment.DoctorId,
                PatientId = appointment.PatientId,
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

            await unitOfWork.MedicalRecords.AddAsync(new MedicalRecord
            {
                Appointment = appointment,
                Doctor = doctor,
                Patient = patient,
                Discription = model.Discription,
                Treatment = model.Treatment,
                Diagnosis = model.Diagnosis,
            });
            await unitOfWork.CompleteAsync();
            return RedirectToAction("Doctor", "MedicalRecord");
        }


        [HttpPost]
        [Authorize(Roles = SD.Doctor)]
        public async Task<IActionResult> Edit(int Id)
        {
            var record = await unitOfWork.MedicalRecords.FindAsync(m => m.Id == Id);
            if (record == null) return View("Error");
            return View(new EditMedicalRecordModel
            {
                Id = Id,
                DoctorId = record.DoctorId,
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
