using Template.Application.Commands.Sample.Delete;

using Xunit;

namespace Template.Application.Tests.Sample.Delete
{
    public class DeleteRequestTests
    {
        [Theory]
        [InlineData(null,  false)]
        [InlineData("", false)]
        [InlineData("1",  true)]
        public void DeleteRequest_Should_Be_Valid(string id, bool expected)
        {
            DeleteRequest req = new DeleteRequest(id);

            Assert.Equal(req.IsValid, expected);
        }
    }
}