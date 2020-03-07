using Application.Commands;

namespace Template.Application.Commands.Sample.Delete
{
    public class SampleDeleted : DomainEvent
    {
        public SampleDeleted(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}