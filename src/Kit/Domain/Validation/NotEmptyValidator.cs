using FluentValidation;
using FluentValidation.Results;

namespace Kit.Domain.Validation
{
    internal class NotEmptyValidator<T> : AbstractValidator<T>
    {
        public NotEmptyValidator()
        {
            RuleFor(x => x)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .NotEmpty();
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