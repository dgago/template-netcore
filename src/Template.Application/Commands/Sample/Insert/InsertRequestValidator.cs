using FluentValidation;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertRequestValidator : AbstractValidator<InsertRequest>
    {
        public InsertRequestValidator()
        {
            this.RuleFor(x => x.Item).NotNull();
            this.RuleFor(x => x.Item.Id).NotEmpty();
            this.RuleFor(x => x.Item.Description).NotEmpty().MaximumLength(250);
        }
    }
}