using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Domain.Validation;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.GetById
{
    public class GetByIdHandler : BaseRequestHandler<GetByIdRequest, EntityResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetByIdHandler(ISampleRepository sampleRepository, IEventPublisher eventPublisher) :
            base(eventPublisher)
        {
            _sampleRepository = sampleRepository;
        }

        protected override async Task<EntityResult<SampleDto>> HandleRequest(
            GetByIdRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(item.Exists());

            return !request.IsValid
                ? new EntityResult<SampleDto>(request.Notifications, null)
                : new EntityResult<SampleDto>(request.Notifications, SampleDto.FromEntity(item));
        }
    }
}