using Application.Models.Result;

namespace Application.Models.Request
{
    public abstract class QueryRequest<T> : Request<QueryResult<T>>
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