using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.Delete
{
    public class DeleteHandler : BaseRequestHandler<DeleteRequest, Result>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteHandler(IEventPublisher eventPublisher, ISampleRepository sampleRepository,
                             IUnitOfWork unitOfWork) : base(eventPublisher)
        {
            _sampleRepository = sampleRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task<Result> Handle(DeleteRequest request,
                                                  CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item = await _sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(Exists(item));

            if (request.IsValid)
            {
                await _sampleRepository.DeleteAsync(item);

                await _unitOfWork.SaveAsync();

                await EventPublisher.Publish(new SampleDeleted(request.Id), cancellationToken);
            }

            return new Result(request.Notifications);
        }
    }
}