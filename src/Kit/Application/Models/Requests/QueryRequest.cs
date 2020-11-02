using Kit.Application.Models.Responses;

namespace Kit.Application.Models.Requests
{
    public abstract class QueryRequest<T> : RequestBase<QueryResult<T>>
        where T : class
    {
        protected QueryRequest(int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}