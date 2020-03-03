using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validation
{
    public class NotEmptyValidator<T> : AbstractValidator<T>
    {
        public NotEmptyValidator()
        {
            this.RuleFor(x => x)
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

//        public override ValidationResult Validate(ValidationContext<T> context)
//        {
//            return context.InstanceToValidate == null
//                ? new ValidationResult(new[]
//                {
//                    new ValidationFailure("Value", "Value cannot be null")
//                })
//                : base.Validate(context.InstanceToValidate);
//        }
    }
}