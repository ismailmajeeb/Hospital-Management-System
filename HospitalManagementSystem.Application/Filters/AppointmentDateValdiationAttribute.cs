using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Filters
{

    public class AppointmentDateValdiationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var date = (DateTime)value;
            return date > DateTime.UtcNow.AddHours(2) ? true : false;
        }
    }
}
