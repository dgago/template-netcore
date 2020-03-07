using FluentValidation;
using FluentValidation.Results;

namespace Template.Domain.Sample
{
    public class DescriptionValidator : AbstractValidator<string>
    {
        public DescriptionValidator()
        {
            RuleFor(x => x).NotEmpty().MaximumLength(250);
        }

        protected override bool PreValidate(ValidationContext<string> context, ValidationResult result)
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