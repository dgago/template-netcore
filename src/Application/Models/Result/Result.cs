using System.Collections.Generic;

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
    }
}