using System.Threading.Tasks;

using Template.Application.Adapters;

namespace Template.Infra.Adapters
{
    public class DummySampleAdapter : ISampleAdapter
    {
        public Task<string> GetRandomDescriptionAsync()
        {
            return Task.FromResult("dummy test");
        }
    }
}