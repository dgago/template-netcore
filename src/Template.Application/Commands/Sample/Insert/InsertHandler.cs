using System.Threading;
using System.Threading.Tasks;

using Application.Commands;
using Application.Models.Result;
using Application.Repositories;

using Domain.Validation;

using MediatR;

using Template.Application.Repositories;
using Template.Application.Services;

namespace Template.Application.Commands.Sample.Insert
{
    public class InsertHandler : BaseRequestHandler<InsertRequest, Result>
    {
        private readonly ISampleRepository _sampleRepository;
        private readonly ISampleService    _sampleService;
        private readonly IUnitOfWork       _unitOfWork;

        public InsertHandler(IMediator eventPublisher,
            ISampleRepository sampleRepository,
            IUnitOfWork unitOfWork,
            ISampleService sampleService) : base(eventPublisher)
        {
            this._sampleRepository = sampleRepository;
            this._unitOfWork       = unitOfWork;
            this._sampleService    = sampleService;
        }

        public override async Task<Result> Handle(InsertRequest request,
            CancellationToken cancellationToken)
        {
            int calc =
                await this._sampleService.CalculateSampleAsync(request.Item.ToEntity());

            // calc must be != 0
            request.AddNotifications(new NotEmptyValidator<int>().Validate(calc));

            if (request.IsValid)
            {
                await this._sampleRepository.InsertAsync(request.Item.ToEntity());

                await this._unitOfWork.SaveAsync();

                await this.EventPublisher.Publish(new SampleInserted(request.Item),
                    cancellationToken);
            }

            return new Result(request.Notifications);
        }
    }
}