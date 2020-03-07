using System.Threading.Tasks;

using Template.Application.Services;

namespace Template.Application.Tests.Sample.Mocks
{
    public class MockSampleService : ISampleService
    {
        public Task<int> CalculateSampleAsync(Domain.Sample.Sample description)
        {
            return Task.FromResult(1);
        }
    }
}