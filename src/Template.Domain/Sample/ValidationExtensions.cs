using System;

using FluentValidation.Results;

namespace Template.Domain.Sample
{
    public static class ValidationExtensions
    {
        public static ValidationResult IsValidDescription(this string arg,
            Action action)
        {
            ValidationResult result = IsValidDescription(arg);
            if (result.IsValid)
            {
                action();
            }

            return result;
        }

        public static ValidationResult IsValidDescription(this string arg)
        {
            return new DescriptionValidator().Validate(arg);
        }
    }
}