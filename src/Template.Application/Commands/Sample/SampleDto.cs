namespace Template.Application.Commands.Sample
{
    public class SampleDto
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public Domain.Sample.Sample ToEntity()
        {
            return new Domain.Sample.Sample(Id, Description);
        }

        public static SampleDto FromEntity(Domain.Sample.Sample item)
        {
            return new SampleDto {Id = item.Id, Description = item.Description};
        }
    }
}