using FluentValidation;
using FluentValidation.Results;

namespace Kit.Domain.Validation
{
    internal class NotNullValidator<T> : AbstractValidator<T>
    {
        public NotNullValidator()
        {
            RuleFor(x => x).NotNull();
        }

        protected override bool PreValidate(ValidationContext<T> context,
            ValidationResult result)
        {
            if (context.InstanceToValidate != null)
            {
                return true;
            }

            result.Errors.Add(new ValidationFailure(nameof(ErrorType.BadRequest),
                "Please ensure a model was supplied."));
            return false;
        }
    }
}