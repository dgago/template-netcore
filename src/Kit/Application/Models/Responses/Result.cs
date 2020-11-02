using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Kit.Application.Models.Responses
{
    public class Result
    {
        public Result(IReadOnlyCollection<ValidationResult> notifications)
        {
            Notifications = notifications;
        }

        public IReadOnlyCollection<ValidationResult> Notifications { get; }

        public bool IsValid
        {
            get
            {
                return Notifications.Count == 0
                       || Notifications.All(x => x.IsValid);
            }
        }
    }
}