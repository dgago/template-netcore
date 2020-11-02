using System.Collections.Generic;

using FluentValidation.Results;

namespace Kit.Application.Models.Responses
{
    public class QueryResult<T> : Result
        where T : class
    {
        public QueryResult(IReadOnlyCollection<ValidationResult> notifications,
            IEnumerable<T> items,
            long count) : base(
            notifications)
        {
            Items = items;
            Count = count;
        }

        public IEnumerable<T> Items { get; }

        public long Count { get; }
    }
}