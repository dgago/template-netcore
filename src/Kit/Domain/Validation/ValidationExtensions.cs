using FluentValidation.Results;

namespace Kit.Domain.Validation
{
    public static class ValidationExtensions
    {
        public static ValidationResult NotEmpty<T>(this T arg)
        {
            return new NotEmptyValidator<T>().Validate(arg);
        }

        public static ValidationResult NotNull<T>(this T arg)
        {
            return new NotNullValidator<T>().Validate(arg);
        }

        public static ValidationResult Exists<T>(this T arg)
            where T : class
        {
            ValidationResult res = new ValidationResult();
            if (arg != null)
            {
                return res;
            }

            ValidationFailure failure =
                new ValidationFailure(nameof(ErrorType.NotFound), "Item was not found.");
            res.Errors.Add(failure);

            return res;
        }
    }
}