using System.Threading.Tasks;

using Template.Domain.Sample;

namespace Template.Application.Services
{
    public interface ISampleService
    {
        Task<int> CalculateSampleAsync(Sample description);
    }
}