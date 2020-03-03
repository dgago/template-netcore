using System.Threading.Tasks;

namespace Template.Application.Adapters
{
    public interface ISampleAdapter
    {
        Task<string> GetRandomDescriptionAsync();
    }
}