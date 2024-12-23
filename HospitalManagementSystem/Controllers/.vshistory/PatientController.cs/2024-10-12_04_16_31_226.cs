﻿




using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

        public PatientController(IUnitOfWork unitOfWork,
                                UserManager<ApplicationUser> userManager)
        {
            this.unitOfWork = unitOfWork;
            this.userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = $"{SD.Admin},{SD.SuperAdmin}")]
        public async Task<IActionResult> Index()
        {
            var temp = await unitOfWork.Patients.GetAllAsync();
            List<PatientsIndexModel> model = temp.Select(p => new PatientsIndexModel { Id = p.Id ,Name = p.Name }).ToList();
            return View(model);

        }
        
        [HttpGet]
        [Authorize(Roles = SD.Patient)]
        public async Task<IActionResult> Dashboard()
        {
            var user = await userManager.GetUserAsync(User);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await unitOfWork.Patients.FindAsync(p => p.UserId == userId);
            var model = new PatientDashBoardModel
            {
                Age = patient.Age,
                Gender = patient.Gender,
                BloodGroup = patient.MedicalRecords?.FirstOrDefault()?.BloodGroup,
                Name = patient.Name,
                NextAppointmentDateTime = patient.Appointments?.FirstOrDefault().DateTime,
                DoctorName = patient.Doctor?.Name,
                MedicalRecordDiscription = patient.MedicalRecords?.Select(m => m.Description).ToList(),
                Allergies = patient.MedicalRecords?.FirstOrDefault()?.Allergies,
                ChronicConditions = patient.MedicalRecords?.FirstOrDefault()?.ChronicConditions,
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
        public async Task<IActionResult> Create(UserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new ApplicationUser()
            {
                Address = model.Address,
                Email = model.Email,
                DateOfbirth = model.DateOfBirth,
                UserName = model.UserName,
                SSN = model.SSN,
                PhoneNumber = model.PhoneNumber,

            };
            var result = await userManager.CreateAsync(user);
            foreach (var e in result.Errors)
                ModelState.AddModelError("", e.Description);
            if (!result.Succeeded) return View(model);

            await unitOfWork.Patients.AddAsync(new()
            {
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Name = model.FirstName + " " + model.LastName,
                User = user,
                Gender = model.Gender,
            });

            await unitOfWork.CompleteAsync();

            var temp = await unitOfWork.Patients.GetAllAsync();
            return View("Index", temp.Select(p => new PatientsIndexModel { Name = p.Name })
                                     .ToList());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Delete(int Id)
        {
            if (!ModelState.IsValid) return View("Error");
            var patient = await unitOfWork.Patients.GetByIdAsync(Id);
            if (patient == null) return View("Error");
            unitOfWork.Patients.Delete(patient);
            await unitOfWork.CompleteAsync();
            var temp = await unitOfWork.Patients.GetAllAsync();
            return View("Index", temp.Select(p => new PatientsIndexModel { Name = p.Name })
                                     .ToList());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Admin)]
        public async Task<IActionResult> Edit(int Id)
        {
            var patient = await unitOfWork.Patients.GetByIdAsync(Id);
            var user =  await userManager.FindByIdAsync(patient.UserId);
            if(user == null || patient == null) return View("Error");
            var model = new EditPatientModel
            {
                Gender = patient.Gender,
                FirstName = patient.Name.TrimStart(' '),
                LastName = patient.Name.TrimEnd(' '),
                Address = user.Address,
                DateOfBirth = user.DateOfbirth.Value,
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
        public async Task<IActionResult> Edit(EditPatientModel model)
        {
            if (!ModelState.IsValid) return View("Error");
            var patient = new Patient()
            {
                Name = model.FirstName + " " + model.LastName,
                Age = DateTime.Now.Year - model.DateOfBirth.Year,
                Gender = model.Gender,
            };
            if (patient == null) return View("Error");
            unitOfWork.Patients.Update(patient);
            await unitOfWork.CompleteAsync();
            return View();
        }
    }
}
