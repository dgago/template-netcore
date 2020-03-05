using System.Collections.Generic;
using System.Linq;

using FluentValidation.Results;

namespace Application.Models.Result
{
    public class EntityResult<T> : Result
        where T : class
    {
        public EntityResult(IReadOnlyCollection<ValidationResult> notifications, T item) :
            base(notifications)
        {
            this.Item = item;
        }

        public T Item { get; }
    }
}