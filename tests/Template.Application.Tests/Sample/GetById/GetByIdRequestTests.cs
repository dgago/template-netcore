using Template.Application.Commands.Sample.GetById;

using Xunit;

namespace Template.Application.Tests.Sample.GetById
{
    public class GetByIdRequestTests
    {
        [Theory]
        [InlineData(null,  false)]
        [InlineData("", false)]
        [InlineData("1",  true)]
        public void GetByIdRequest_Should_Be_Valid(string id, bool expected)
        {
            GetByIdRequest req = new GetByIdRequest(id);

            Assert.Equal(req.IsValid, expected);
        }
    }
}