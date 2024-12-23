﻿using HospitalManagementSystem.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagementSystem.Controllers
{
    public class PatientController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public PatientController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]

        public IActionResult Dashboard()
        {
            return View(new Patient { Appointments = new(),MedicalRecords = new()});
        }
    }
}
