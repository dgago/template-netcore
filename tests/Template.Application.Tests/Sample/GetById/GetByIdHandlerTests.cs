using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentValidation.Results;

using Kit.Application.Models.Responses;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Commands.Sample.GetById;
using Template.Bootstrap;
using Template.Infra.Repositories;

using Test;
using Test.Mocks;

using Xunit;

namespace Template.Application.Tests.Sample.GetById
{
    public class GetByIdHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1", true)]
        public async void GetByIdHandler_Should_Work(string id, bool expected)
        {
            // Given
            IMediator mediator = ServiceProvider.GetService<IMediator>();

            MockEventPublisher publisher = new MockEventPublisher(mediator);
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>
                {
                    {"1", new Domain.Sample.Sample("1", "1")}
                });

            GetByIdHandler handler = new GetByIdHandler(repository, publisher);

            GetByIdRequest command = new GetByIdRequest(id);

            // When
            Result result = await handler.Handle(command, new CancellationToken());

            // Then
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notif => !notif.IsValid).ToList();
            if (expected)
            {
                Assert.Empty(notValidNotifications);
            }
            else
            {
                Assert.NotEmpty(notValidNotifications);
            }
        }

        protected override void AddServices(IServiceCollection services,
            IConfiguration configuration)
        {
            Services.ConfigureTemplateServices(configuration);
        }
    }
}