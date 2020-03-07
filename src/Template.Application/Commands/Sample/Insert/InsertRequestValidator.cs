using FluentValidation;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertRequestValidator : AbstractValidator<InsertRequest>
    {
        public InsertRequestValidator()
        {
            RuleFor(x => x.Item).NotNull();
            RuleFor(x => x.Item.Id).NotEmpty();
            RuleFor(x => x.Item.Description).NotEmpty().MaximumLength(250);
        }
    }
}