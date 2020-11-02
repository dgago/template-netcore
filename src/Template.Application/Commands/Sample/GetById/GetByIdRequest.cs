using Kit.Application.Models.Requests;
using Kit.Domain.Validation;

namespace Template.Application.Commands.Sample.GetById
{
    public class GetByIdRequest : EntityRequest<SampleDto>
    {
        public GetByIdRequest(string id)
        {
            Id = id;

            AddNotifications(id.NotEmpty());
        }

        public string Id { get; }
    }
}