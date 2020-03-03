using Domain;
using Domain.Validation;

using FluentValidation.Results;

namespace Template.Domain.Sample
{
    public class Sample : Entity
    {
        public Sample(string id, string description)
        {
            // TODO: cómo validar acá? Ideas: tener un miembro público IsValid y una coleccion de Notif
            this.Notifications.Add(new NotEmptyValidator<string>().Validate(id));
            this.Notifications.Add(new DescriptionValidator().Validate(description));

            if (this.IsValid)
            {
                this.Id          = id;
                this.Description = description;
            }
        }

        public string Id { get; }

        public string Description { get; private set; }

        public ValidationResult ChangeDescription(string description)
        {
            ValidationResult res = new DescriptionValidator().Validate(description);

            if (res.IsValid)
            {
                this.Description = description;
            }

            return res;
        }
    }
}