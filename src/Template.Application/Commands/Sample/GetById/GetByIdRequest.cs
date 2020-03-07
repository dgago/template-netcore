using Application.Models.Request;

using Domain.Validation;

namespace Template.Application.Commands.Sample.GetById
{
    public class GetByIdRequest : EntityRequest<SampleDto>
    {
        public GetByIdRequest(string id)
        {
            Id = id;

            AddNotifications(new NotEmptyValidator<string>().Validate(id));
        }

        public string Id { get; }
    }
}