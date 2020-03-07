using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

using MediatR;

namespace Application.Models.Request
{
    public abstract class Request<T> : IRequest<T>
        where T : Result.Result
    {
        protected Request()
        {
            Notifications = new List<ValidationResult>();
        }

        public bool IsValid
        {
            get { return Notifications.All(x => x.IsValid); }
        }

        public List<ValidationResult> Notifications { get; }

        public void AddNotifications(params ValidationResult[] items)
        {
            Notifications.AddRange(items);
        }
    }
}