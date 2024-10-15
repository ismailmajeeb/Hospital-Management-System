using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Filters
{
    public class DateOfBirthAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var dateOfBirth = (DateTime)value;
            return dateOfBirth < DateTime.MinValue || dateOfBirth > DateTime.UtcNow ? false : true;
        }


    }
}
