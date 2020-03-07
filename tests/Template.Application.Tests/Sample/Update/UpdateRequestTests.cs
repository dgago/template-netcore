using Template.Application.Commands.Sample.Update;

using Xunit;

namespace Template.Application.Tests.Sample.Update
{
    public class UpdateRequestTests
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
        public void UpdateRequest_Should_Be_Valid(string id, string description, bool expected)
        {
            UpdateRequest req = new UpdateRequest(id, description);

            Assert.Equal(req.IsValid, expected);
        }
    }
}