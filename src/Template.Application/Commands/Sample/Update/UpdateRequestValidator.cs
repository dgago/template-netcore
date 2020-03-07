using FluentValidation;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
        }
    }
}