using System.Collections.Generic;

using FluentValidation.Results;

namespace Kit.Application.Models.Responses
{
    public class EntityResult<T> : Result
        where T : class
    {
        public EntityResult(IReadOnlyCollection<ValidationResult> notifications, T item) :
            base(notifications)
        {
            Item = item;
        }

        public T Item { get; }
    }
}