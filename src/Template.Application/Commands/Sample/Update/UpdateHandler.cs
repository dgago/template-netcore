using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using Domain.Validation;

using MediatR;

using Template.Application.Adapters;
using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Update
{
    public class UpdateHandler : BaseRequestHandler<UpdateRequest, Result>
    {
        private readonly ISampleAdapter    _sampleAdapter;
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork       _unitOfWork;

        public UpdateHandler(IMediator eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ISampleAdapter sampleAdapter) : base(eventPublisher)
        {
            this._sampleRepository = sampleRepository;
            this._unitOfWork       = unitOfWork;
            this._sampleAdapter    = sampleAdapter;
        }

        public override async Task<Result> Handle(UpdateRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item =
                await this._sampleRepository.GetByIdAsync(request.Id);

            // sample must exist
            request.AddNotifications(
                new NotNullValidator<Domain.Sample.Sample>().Validate(item));

            // gets random desc from adapter
            string randomDesc = await this._sampleAdapter.GetRandomDescriptionAsync();

            // random desc should contain a non-empty string
            request.AddNotifications(new NotEmptyValidator<string>().Validate(randomDesc));

            string newDescription = $"{request.Description} {randomDesc}";

            // attempts to change description and saves result in the notifications collection
            request.AddNotifications(item.ChangeDescription(newDescription));

            if (request.IsValid)
            {
                await this._sampleRepository.UpdateAsync(item);

                await this._unitOfWork.SaveAsync();

                await this.EventPublisher.Publish(
                    new SampleUpdated(request.Id, newDescription),
                    cancellationToken);
            }

            return new Result(request.Notifications);
        }
    }
}