using FluentValidation.Results;

using Kit.Domain;
using Kit.Domain.Validation;

namespace Template.Domain.Sample
{
    public class Sample : Entity
    {
        public Sample(string id, string description)
        {
            Notifications.Add(id.NotEmpty());
            Notifications.Add(description.IsValidDescription());

            if (IsValid)
            {
                Id = id;
                Description = description;
            }
        }

        public string Id { get; }

        public string Description { get; private set; }

        public ValidationResult ChangeDescription(string description)
        {
            return description
                .IsValidDescription(() => Description = description);
        }
    }
}