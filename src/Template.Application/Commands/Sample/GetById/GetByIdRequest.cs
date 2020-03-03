using Application.Models.Request;

using Domain.Validation;

namespace Template.Application.Commands.Sample.GetById
{
    public class GetByIdRequest : EntityRequest<SampleDto>
    {
        public GetByIdRequest(string id)
        {
            this.Id = id;

            this.AddNotifications(new NotEmptyValidator<string>().Validate(id));
        }

        public string Id { get; }
    }
}