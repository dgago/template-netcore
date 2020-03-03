using Application.Models.Request;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateRequest : CommandRequest
    {
        public UpdateRequest(string id, string description)
        {
            this.Id = id;
            this.Description = description;

            this.AddNotifications(new UpdateRequestValidator().Validate(this));
        }

        public string Id { get; }

        public string Description { get; }
    }
}