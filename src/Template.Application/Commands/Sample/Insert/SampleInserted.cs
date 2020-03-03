using Application.Commands;

namespace Template.Application.Commands.Sample.Insert
{
    public class SampleInserted : DomainEvent
    {
        public SampleInserted(SampleDto item)
        {
            this.Item = item;
        }

        public SampleDto Item { get; set; }
    }
}