using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Application.Models.Result
{
    public class Result
    {
        public Result(IReadOnlyCollection<ValidationResult> notifications)
        {
            this.Notifications = notifications;
        }

        public IReadOnlyCollection<ValidationResult> Notifications { get; }

        public bool IsValid
        {
            get
            {
                return this.Notifications.Count == 0
                    || this.Notifications.All(x => x.IsValid);
            }
        }
    }
}