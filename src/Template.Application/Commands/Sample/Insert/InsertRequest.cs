using Application.Models.Request;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertRequest : CommandRequest
    {
        public InsertRequest(SampleDto item)
        {
            this.Item = item;

            this.AddNotifications(new InsertRequestValidator().Validate(this));
        }

        public SampleDto Item { get; }
    }
}