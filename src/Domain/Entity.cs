using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            this.Notifications = new List<ValidationResult>();
        }

        public List<ValidationResult> Notifications { get; }

        public bool IsValid
        {
            get
            {
                return this.Notifications.All(x => x.IsValid);
            }
        }

        public void AddNotifications(params ValidationResult[] items)
        {
            this.Notifications.AddRange(items);
        }
    }
}