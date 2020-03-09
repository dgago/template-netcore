using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using FluentValidation.Results;

using Template.Application.Adapters;
using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateHandler : BaseRequestHandler<UpdateRequest, Result>
    {
        private readonly ISampleAdapter _sampleAdapter;
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateHandler(IEventPublisher eventPublisher, ISampleRepository sampleRepository,
                             IUnitOfWork unitOfWork, ISampleAdapter sampleAdapter) : base(
            eventPublisher)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork = unitOfWork;
            _sampleAdapter = sampleAdapter;
        }

        public override async Task<Result> Handle(UpdateRequest request,
                                                  CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            // sample must exist
            ValidationResult validationResult = NotNull(item);
            request.AddNotifications(validationResult);

            if (!request.IsValid)
            {
                return new Result(request.Notifications);
            }

            // gets random desc from adapter
            string randomDesc = await _sampleAdapter.GetRandomDescriptionAsync();

            // random desc should contain a non-empty string
            request.AddNotifications(NotEmpty(randomDesc));

            string newDescription = $"{request.Description} {randomDesc}";

            // attempts to change description and saves result in the notifications collection
            request.AddNotifications(item.ChangeDescription(newDescription));

            if (request.IsValid)
            {
                await _sampleRepository.UpdateAsync(item);

                await _unitOfWork.SaveAsync();

                await EventPublisher.Publish(new SampleUpdated(request.Id, newDescription),
                                             cancellationToken);
            }

            return new Result(request.Notifications);
        }
    }
}