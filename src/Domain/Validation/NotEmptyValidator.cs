using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validation
{
    public class NotEmptyValidator<T> : AbstractValidator<T>
    {
        public NotEmptyValidator()
        {
            RuleFor(x => x)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull()
                .NotEmpty();
        }

        protected override bool PreValidate(ValidationContext<T> context,
            ValidationResult result)
        {
            if (context.InstanceToValidate == null)
            {
                result.Errors.Add(new ValidationFailure("",
                    "Please ensure a model was supplied."));
                return false;
            }

            return true;
        }
    }
}