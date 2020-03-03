using Application.Models.Request;

using Domain.Validation;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteRequest : CommandRequest
    {
        public DeleteRequest(string id)
        {
            this.Id = id;

            this.AddNotifications(new NotEmptyValidator<string>().Validate(id));
        }

        public string Id { get; }
    }
}