using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Application.Models.Result;

using MediatR;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Find
{
    public class FindHandler : IRequestHandler<FindRequest, QueryResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;

        public FindHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<QueryResult<SampleDto>> Handle(FindRequest request,
            CancellationToken cancellationToken)
        {
            (IEnumerable<SampleDto> items, long count) =
                await _sampleRepository.FindAsync(request.Id, request.Description);

            return new QueryResult<SampleDto>(request.Notifications, items, count);
        }
    }
}