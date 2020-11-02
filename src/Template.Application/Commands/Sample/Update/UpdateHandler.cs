using System.Threading;
using System.Threading.Tasks;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Application.Repositories;
using Kit.Domain.Validation;

using Template.Application.Adapters;
using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateHandler : BaseRequestHandler<UpdateRequest, Result>
    {
        private readonly ISampleAdapter _sampleAdapter;
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHandler(IEventPublisher eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ISampleAdapter sampleAdapter) : base(
            eventPublisher)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork = unitOfWork;
            _sampleAdapter = sampleAdapter;
        }

        protected override async Task<Result> HandleRequest(UpdateRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            // sample must exist
            request.AddNotifications(item.Exists());

            if (!request.IsValid)
            {
                return request.GetResult();
            }

            // gets random desc from adapter
            string randomDesc = await _sampleAdapter.GetRandomDescriptionAsync();

            // random desc should contain a non-empty string
            request.AddNotifications(randomDesc.NotEmpty());

            string newDescription = $"{request.Description} {randomDesc}";

            // attempts to change description and saves result in the notifications collection
            request.AddNotifications(item.ChangeDescription(newDescription));

            if (!request.IsValid)
            {
                return request.GetResult();
            }

            await _sampleRepository.UpdateAsync(item);

            await _unitOfWork.SaveAsync();

            return request.GetResult();
        }
    }
}