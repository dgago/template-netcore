using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validation
{
    public class NotNullValidator<T> : AbstractValidator<T>
    {
        public NotNullValidator()
        {
            this.RuleFor(x => x).NotNull();
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