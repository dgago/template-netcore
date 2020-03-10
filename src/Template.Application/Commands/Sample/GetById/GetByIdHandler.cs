using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;

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

        public override async Task<EntityResult<SampleDto>> Handle(
            GetByIdRequest request, CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(Exists(item));

            return !request.IsValid
                ? new EntityResult<SampleDto>(request.Notifications, null)
                : new EntityResult<SampleDto>(request.Notifications, SampleDto.FromEntity(item));
        }
    }
}