using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

public class UsernameAttribute : ValidationAttribute
{
    private const int MinLength = 3;
    private const int MaxLength = 20;

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string username = value.ToString();

            // Check length
            if (username.Length < MinLength || username.Length > MaxLength)
            {
                return new ValidationResult($"Username must be between {MinLength} and {MaxLength} characters.");
            }

            // Check allowed characters (letters, numbers, underscores, hyphens)
            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_-]+$"))
            {
                return new ValidationResult("Username can only contain letters, numbers, underscores, and hyphens.");
            }

            // Check uniqueness (assuming a UserService is available to check the database)
            var userService = (UserService)validationContext.GetService(typeof(UserService));
            if (userService != null && userService.IsUsernameTaken(username))
            {
                return new ValidationResult("Username is already taken.");
            }
        }

        return ValidationResult.Success;
    }
}
