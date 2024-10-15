﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class OneWordAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value != null)
        {
            string input = value.ToString();
            if (!Regex.IsMatch(input, @"^\S+\s*$") // \S+ means one or more non-whitespace characters
            {
                return new ValidationResult("The field must contain exactly one word.");
            }
        }

        return ValidationResult.Success;
    }
}
