using System.Threading;
using System.Threading.Tasks;

using Application.Models.Result;

using Domain.Validation;

using MediatR;

using Template.Application.Repositories;

namespace Template.Application.Commands.Sample.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, EntityResult<SampleDto>>
    {
        private readonly ISampleRepository _sampleRepository;

        public GetByIdHandler(ISampleRepository sampleRepository)
        {
            _sampleRepository = sampleRepository;
        }

        public async Task<EntityResult<SampleDto>> Handle(GetByIdRequest request,
            CancellationToken cancellationToken)
        {
            Domain.Sample.Sample item =
                await _sampleRepository.GetByIdAsync(request.Id);

            request.AddNotifications(
                new NotNullValidator<Domain.Sample.Sample>().Validate(item));

            return new EntityResult<SampleDto>(request.Notifications,
                SampleDto.FromEntity(item));
        }
    }
}