using Template.Application.Commands.Sample;
using Template.Application.Commands.Sample.Insert;

using Xunit;

namespace Template.Application.Tests.Sample.Insert
{
    public class InsertRequestTests
    {
        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("1", "2", true)]
        [InlineData("1",
                    "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                    false)]
        public void InsertRequest_Should_Be_Valid(string id, string description, bool expected)
        {
            SampleDto item = new SampleDto {Id = id, Description = description};

            InsertRequest req = new InsertRequest(item);

            Assert.Equal(req.IsValid, expected);
        }
    }
}