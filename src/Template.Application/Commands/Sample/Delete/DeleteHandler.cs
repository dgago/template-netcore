using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Application.Repositories;
using Kit.Domain.Validation;

using Microsoft.Extensions.Logging;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteHandler : BaseRequestHandler<DeleteRequest, Result>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHandler(IEventPublisher eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ILogger<DeleteRequest> logger) : base(eventPublisher, logger)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork = unitOfWork;
        }

        protected override async Task<Result> HandleRequest(DeleteRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(item.Exists());

            if (request.IsValid)
            {
                await _sampleRepository.DeleteAsync(item);

                await _unitOfWork.SaveAsync();
            }

            return request.GetResult();
        }
    }
}