using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using Domain.Validation;

using Template.Application.Repositories;
using Template.Application.Services;

namespace Template.Application.Commands.Sample.Insert
{
    public class
        InsertHandler : BaseRequestHandler<InsertRequest, EntityResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ISampleService    _sampleService;
        private readonly IUnitOfWork       _unitOfWork;

        public InsertHandler(IEventPublisher eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ISampleService sampleService) : base(eventPublisher)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork       = unitOfWork;
            _sampleService    = sampleService;
        }

        public override async Task<EntityResult<SampleDto>> Handle(InsertRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                return new EntityResult<SampleDto>(request.Notifications, request.Item);
            }

            int calc =
                await _sampleService.CalculateSampleAsync(request.Item.ToEntity());

            // calc must be != 0
            request.AddNotifications(new NotEmptyValidator<int>().Validate(calc));

            if (!request.IsValid)
            {
                return new EntityResult<SampleDto>(request.Notifications, request.Item);
            }

                await _sampleRepository.InsertAsync(request.Item.ToEntity());

                await _unitOfWork.SaveAsync();

                await EventPublisher.Publish(new SampleInserted(request.Item),
                    cancellationToken);

            return new EntityResult<SampleDto>(request.Notifications, request.Item);
        }
    }
}