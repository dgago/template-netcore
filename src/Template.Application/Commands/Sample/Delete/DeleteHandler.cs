using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using Domain.Validation;

using MediatR;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteHandler : BaseRequestHandler<DeleteRequest, Result>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork       _unitOfWork;

        public DeleteHandler(IMediator eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork) : base(eventPublisher)
        {
            this._sampleRepository = sampleRepository;
            this._unitOfWork       = unitOfWork;
        }

        public override async Task<Result> Handle(DeleteRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item =
                await this._sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(
                new NotNullValidator<Domain.Sample.Sample>().Validate(item));

            if (request.IsValid)
            {
                await this._sampleRepository.DeleteAsync(item);

                await this._unitOfWork.SaveAsync();

                await this.EventPublisher.Publish(new SampleDeleted(request.Id),
                    cancellationToken);
            }

            return new Result(request.Notifications);
        }
    }
}