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
            var dateOfBirth = (DateTime)value;
            return dateOfBirth < DateTime.MinValue || dateOfBirth > DateTime.UtcNow ? false : true;
        }
    }
}
