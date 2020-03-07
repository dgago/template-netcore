using Application.Models.Request;

using Domain.Validation;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteRequest : CommandRequest
    {
        public DeleteRequest(string id)
        {
            Id = id;

            AddNotifications(new NotEmptyValidator<string>().Validate(id));
        }

        public string Id { get; }
    }
}