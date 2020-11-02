using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Find
{
    public class FindHandler : BaseRequestHandler<FindRequest, QueryResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;

        public FindHandler(ISampleRepository sampleRepository, IEventPublisher eventPublisher) :
            base(eventPublisher)
        {
            _sampleRepository = sampleRepository;
        }

        protected override async Task<QueryResult<SampleDto>> HandleRequest(
            FindRequest request, CancellationToken cancellationToken)
        {
            (IEnumerable<SampleDto> items, long count) =
                await _sampleRepository.FindAsync(request.Id,
                                                  request.Description,
                                                  request.PageIndex,
                                                  request.PageSize);

            return new QueryResult<SampleDto>(request.Notifications, items, count);
        }
    }
}