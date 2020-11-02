using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentValidation.Results;

using Kit.Application.Models.Responses;
using Kit.Application.Repositories;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Moq;

using Template.Application.Commands.Sample.Delete;
using Template.Bootstrap;
using Template.Infra.Repositories;

using Test;
using Test.Mocks;

using Xunit;

namespace Template.Application.Tests.Sample.Delete
{
    public class DeleteHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("", false)]
        [InlineData("1", true)]
        public async void DeleteHandler_Should_Work(string id, bool expected)
        {
            // Given
            IMediator mediator = ServiceProvider.GetService<IMediator>();

            MockEventPublisher publisher = new MockEventPublisher(mediator);
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>
                {
                    {"1", new Domain.Sample.Sample("1", "1")}
                });
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();

            DeleteHandler handler = new DeleteHandler(publisher, repository, uow.Object);

            DeleteRequest command = new DeleteRequest(id);

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