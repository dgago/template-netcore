using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Template.Application.Commands.Sample;
using Template.Domain.Sample;

namespace Template.Application.Repositories
{
    public interface ISampleRepository
    {
        Task InsertAsync(Sample item);

        Task<Sample> GetByIdAsync(string id);

        Task DeleteAsync(Sample item);

        Task UpdateAsync(Sample item);

        Task<Tuple<IEnumerable<SampleDto>, long>> FindAsync(string id,
            string description);
    }
}