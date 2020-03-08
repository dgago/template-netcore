using Template.Application.Commands.Sample.Find;

using Xunit;

namespace Template.Application.Tests.Sample.Find
{
    public class FindRequestTests
    {
        [Theory]
        [InlineData(null, null, true)]
        [InlineData("", "", true)]
        [InlineData("1", "1",  true)]
        public void FindRequest_Should_Be_Valid(string id, string description, bool expected)
        {
            FindRequest req = new FindRequest(id, description, 1, 3);

            Assert.Equal(req.IsValid, expected);
        }
    }
}