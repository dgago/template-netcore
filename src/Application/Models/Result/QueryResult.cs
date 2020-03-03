using System.Collections.Generic;

using FluentValidation.Results;

namespace Application.Models.Result
{
    public class QueryResult<T> : Result
        where T : class
    {
        public QueryResult(IReadOnlyCollection<ValidationResult> notifications,
            IEnumerable<T> items,
            long count) : base(
            notifications)
        {
            this.Items = items;
            this.Count = count;
        }
        
        public IEnumerable<T> Items { get; }
        
        public long Count { get; }
    }
}