using HospitalManagementSystem.Core.Entities;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace HospitalManagementSystem.Application.Filters
{
    public class UniqueUsernameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var userManager = (UserManager<ApplicationUser>)validationContext.GetService(typeof(UserManager<ApplicationUser>));
            var userId = (string)validationContext.ObjectType.GetProperty("Id").GetValue(validationContext.ObjectInstance, null);
            var username = value as string;

            var existingUser = userManager.FindByNameAsync(username).Result;
            if (existingUser != null && existingUser.Id != userId)
            {
                return new ValidationResult("User Name is already taken.");
            }

            return ValidationResult.Success;
        }
    }
}
