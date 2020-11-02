using Kit.Application.Models.Requests;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertRequest : EntityRequest<SampleDto>
    {
        public InsertRequest(SampleDto item)
        {
            Item = item;

            AddNotifications(new InsertRequestValidator().Validate(this));
        }

        public SampleDto Item { get; }
    }
}