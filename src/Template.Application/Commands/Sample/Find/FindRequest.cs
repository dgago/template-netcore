using Application.Models.Request;

namespace Template.Application.Commands.Sample.Find
{
    public class FindRequest : QueryRequest<SampleDto>
    {
        public FindRequest(string id, string description, uint pageIndex, uint pageSize) :
            base(pageIndex, pageSize)
        {
            Id          = id;
            Description = description;
        }

        public string Id { get; }

        public string Description { get; }
    }
}