using HospitalManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Application.Filters
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var userId = (string)validationContext.ObjectType.GetProperty("Id").GetValue(validationContext.ObjectInstance, null);
            var email = value as string;

            var existingUser = userManager.FindByEmailAsync(email).Result;
            if (existingUser != null && existingUser.Id != userId)
            {
                return new ValidationResult("Email is already in use.");
            }

            return ValidationResult.Success;
        }
    }

}
