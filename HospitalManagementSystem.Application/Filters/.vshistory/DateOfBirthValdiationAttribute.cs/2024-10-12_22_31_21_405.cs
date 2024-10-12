using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Filters
{
    public class DateOfBirthValdiationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            var dateOfBirth = (DateTime)value;
            
            if(dateOfBirth < DateTime.MinValue || dateOfBirth > DateTime.MaxValue) 
                return false;
            return true;
        }


    }
}
