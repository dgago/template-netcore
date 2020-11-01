using Application.Commands;

namespace Template.Application.Commands.Sample.Update
{
    public class SampleUpdated : DomainEvent
    {
        public SampleUpdated(string id, string description)
        {
            Id = id;
            Description = description;
        }

        public string Description { get; set; }

        public string Id { get; set; }
    }
}