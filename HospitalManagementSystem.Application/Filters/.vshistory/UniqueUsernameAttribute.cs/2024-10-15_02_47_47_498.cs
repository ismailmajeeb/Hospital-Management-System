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
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var username = value as string;

            var existingUser = userManager.FindByNameAsync(username).Result;
            if (existingUser != null)
            {
                return new ValidationResult("Username is already taken.");
            }

            return ValidationResult.Success;
        }
    }

}
