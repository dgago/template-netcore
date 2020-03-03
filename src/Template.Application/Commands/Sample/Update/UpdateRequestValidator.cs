using FluentValidation;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateRequestValidator : AbstractValidator<UpdateRequest>
    {
        public UpdateRequestValidator()
        {
            this.RuleFor(x => x.Id).NotEmpty();
            this.RuleFor(x => x.Description).NotEmpty().MaximumLength(250);
        }
    }
}