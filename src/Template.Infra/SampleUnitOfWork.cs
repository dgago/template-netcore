using System.Threading.Tasks;

using Application.Repositories;

namespace Template.Infra
{
    public class SampleUnitOfWork : IUnitOfWork
    {
        public Task SaveAsync()
        {
            return Task.CompletedTask;
        }
    }
}