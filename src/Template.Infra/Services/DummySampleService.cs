using System.Threading.Tasks;

using Template.Application.Services;
using Template.Domain.Sample;

namespace Template.Infra.Services
{
    public class DummySampleService : ISampleService
    {
        public Task<int> CalculateSampleAsync(Sample description)
        {
            return Task.FromResult(1);
        }
    }
}