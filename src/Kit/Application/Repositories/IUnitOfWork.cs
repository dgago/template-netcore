using System.Threading.Tasks;

namespace Kit.Application.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}