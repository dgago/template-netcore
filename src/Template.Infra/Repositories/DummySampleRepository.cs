using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Template.Application.Commands.Sample;
using Template.Application.Repositories;
using Template.Domain.Sample;

namespace Template.Infra.Repositories
{
    public class DummySampleRepository : ISampleRepository
    {
        private readonly IDictionary<string, Sample> _db;

        public DummySampleRepository(IDictionary<string, Sample> db)
        {
            this._db = db ?? new Dictionary<string, Sample>();
        }

        public Task InsertAsync(Sample item)
        {
            this._db.Add(item.Id, item);

            return Task.CompletedTask;
        }

        public Task<Sample> GetByIdAsync(string id)
        {
            return Task.FromResult(this._db[id]);
        }

        public Task DeleteAsync(Sample item)
        {
            if (this._db.ContainsKey(item.Id))
            {
                this._db.Remove(item.Id);
            }

            return Task.CompletedTask;
        }

        public Task UpdateAsync(Sample item)
        {
            if (this._db.ContainsKey(item.Id))
            {
                this._db[item.Id] = item;
            }

            return Task.CompletedTask;
        }

        public Task<Tuple<IEnumerable<SampleDto>, long>> FindAsync(string id,
            string description)
        {
            return Task.FromResult(new Tuple<IEnumerable<SampleDto>, long>(
                this._db.Select(x => SampleDto.FromEntity(x.Value)).ToList(),
                this._db.Count));
        }
    }
}