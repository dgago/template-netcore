using Kit.Application.Models.Requests;
using Kit.Domain.Validation;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteRequest : CommandRequest
    {
        public DeleteRequest(string id)
        {
            Id = id;

            AddNotifications(id.NotEmpty());
        }

        public string Id { get; }
    }
}