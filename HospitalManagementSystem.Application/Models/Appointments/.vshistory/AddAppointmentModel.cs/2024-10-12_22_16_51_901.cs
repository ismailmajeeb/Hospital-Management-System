﻿using HospitalManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Models.Appointments
{
    public class AddAppointmentModel
    {
        public string PatientUserName { get; set; }
        public string DoctorUserName { get; set; }

        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
        public Status Status { get; set; }
    }
}
