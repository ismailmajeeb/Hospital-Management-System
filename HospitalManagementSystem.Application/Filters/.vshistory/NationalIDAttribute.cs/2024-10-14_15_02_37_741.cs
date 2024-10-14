using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class SSNAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            // Regular expression for SSN in the format XXX-XX-XXXX
            var ssnPattern = @"^\d{3}-\d{2}-\d{4}$";

            if (!Regex.IsMatch(input, ssnPattern))
            {
                return new ValidationResult("Invalid SSN format. The correct format is XXX-XX-XXXX.");
            }
        }

        return ValidationResult.Success;
    }
}
