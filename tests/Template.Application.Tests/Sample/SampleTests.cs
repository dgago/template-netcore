using Xunit;

namespace Template.Application.Tests.Sample
{
    public class SampleTests
    {
        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("1", "2", true)]
        public void Sample_Should_Be_Valid(string id, string description, bool expected)
        {
            Domain.Sample.Sample sample = new Domain.Sample.Sample(id, description);

            Assert.Equal(sample.IsValid, expected);
        }
    }
}
