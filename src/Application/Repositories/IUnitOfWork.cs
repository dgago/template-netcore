using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}