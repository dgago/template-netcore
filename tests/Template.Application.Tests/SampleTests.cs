using Template.Domain.Sample;

using Xunit;

namespace Template.Application.Tests
{
    public class SampleTests
    {
        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("1", "2", true)]
        public void Sample_Should_Not_Be_Invalid(string id, string description, bool expected)
        {
            Sample sample = new Sample(id, description);

            Assert.Equal(sample.IsValid, expected);
        }
    }
}
