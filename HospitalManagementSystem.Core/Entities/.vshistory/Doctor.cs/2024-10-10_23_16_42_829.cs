﻿

namespace HospitalManagementSystem.Core.Entities
{
    public class Doctor
    {
        int Id {  get; set; }
        public List<Appointment> Appointments {  get; set; }
    }
}
