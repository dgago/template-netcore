using Application.Models.Result;

namespace Application.Models.Request
{
    public abstract class QueryRequest<T> : Request<QueryResult<T>>
        where T : class
    {
        protected QueryRequest(uint pageIndex, uint pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public uint PageIndex { get; set; }

        public uint PageSize { get; set; }
    }
}