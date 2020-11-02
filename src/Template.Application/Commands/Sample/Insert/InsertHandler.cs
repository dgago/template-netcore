using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Application.Repositories;
using Kit.Domain.Validation;

using Microsoft.Extensions.Logging;

using Template.Application.Repositories;
using Template.Application.Services;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertHandler : BaseRequestHandler<InsertRequest, EntityResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ISampleService _sampleService;
        private readonly IUnitOfWork _unitOfWork;

        public InsertHandler(IEventPublisher eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ISampleService sampleService,
            ILogger<InsertRequest> logger) : base(eventPublisher, logger)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork = unitOfWork;
            _sampleService = sampleService;
        }

        protected override async Task<EntityResult<SampleDto>> HandleRequest(InsertRequest request,
            CancellationToken cancellationToken)
        {
            if (!request.IsValid)
            {
                return request.GetResult(request.Item);
            }

            // TODO: validate that the entity does not exist or implement idempotence

            int calc =
                await _sampleService.CalculateSampleAsync(request.Item.ToEntity());

            // calc must be != 0
            request.AddNotifications(calc.NotEmpty());

            if (!request.IsValid)
            {
                return request.GetResult(request.Item);
            }

            await _sampleRepository.InsertAsync(request.Item.ToEntity());

            await _unitOfWork.SaveAsync();

            return request.GetResult(request.Item);
        }
    }
}