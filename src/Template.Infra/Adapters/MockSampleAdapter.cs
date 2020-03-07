using System.Threading.Tasks;

using Template.Application.Adapters;

namespace Template.Infra.Adapters
{
    public class MockSampleAdapter : ISampleAdapter
    {
        public Task<string> GetRandomDescriptionAsync()
        {
            return Task.FromResult("dummy test");
        }
    }
}