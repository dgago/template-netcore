using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Domain
{
    public abstract class Entity
    {
        protected Entity()
        {
            Notifications = new List<ValidationResult>();
        }

        public List<ValidationResult> Notifications { get; }

        public bool IsValid
        {
            get
            {
                return Notifications.All(x => x.IsValid);
            }
        }

/*
        public void AddNotifications(params ValidationResult[] items)
        {
            Notifications.AddRange(items);
        }
*/
    }
}