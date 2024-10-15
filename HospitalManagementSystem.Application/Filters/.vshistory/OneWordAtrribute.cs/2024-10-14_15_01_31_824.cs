using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class OneWordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            // Use a regex to check if it's a single word
            if (!Regex.IsMatch(input, @"^\S+$")) // \S+ means one or more non-whitespace characters
            {
                return new ValidationResult("The field must contain exactly one word.");
            }
        }

        return ValidationResult.Success;
    }
}
