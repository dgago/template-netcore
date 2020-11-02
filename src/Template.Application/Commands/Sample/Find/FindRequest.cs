using Kit.Application.Models.Requests;

namespace Template.Application.Commands.Sample.Find
{
    public class FindRequest : QueryRequest<SampleDto>
    {
        public FindRequest(string id, string description, int pageIndex, int pageSize) : base(
            pageIndex,
            pageSize)
        {
            Id = id;
            Description = description;
        }

        public string Id { get; }

        public string Description { get; }
    }
}