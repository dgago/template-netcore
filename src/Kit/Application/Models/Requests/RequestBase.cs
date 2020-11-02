using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

using Kit.Application.Models.Responses;

using MediatR;

namespace Kit.Application.Models.Requests
{
    public abstract class RequestBase<T> : IRequest<T>
        where T : Result
    {
        protected RequestBase()
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

        public Result GetResult()
        {
            return new Result(Notifications);
        }
    }
}