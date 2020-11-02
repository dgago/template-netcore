using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentValidation.Results;

using Kit.Application.Handlers;
using Kit.Application.Models.Responses;
using Kit.Application.Repositories;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Moq;

using Template.Application.Commands.Sample.Update;
using Template.Bootstrap;
using Template.Infra.Adapters;
using Template.Infra.Repositories;

using Test;
using Test.Mocks;

using Xunit;

namespace Template.Application.Tests.Sample.Update
{
    public class UpdateHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("1", "2", true)]
        public async void UpdateHandler_Should_Work(string id, string description, bool expected)
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
            MockSampleAdapter adapter = new MockSampleAdapter();
            Mock<ILogger<UpdateRequest>> logger = new Mock<ILogger<UpdateRequest>>();

            UpdateHandler handler =
                new UpdateHandler(publisher, repository, uow.Object, adapter, logger.Object);

            UpdateRequest command = new UpdateRequest(id, description);

            // When
            Result result = await handler.Handle(command, new CancellationToken());

            // Then
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notification => !notification.IsValid).ToList();
            if (expected)
            {
                Assert.Empty(notValidNotifications);
                Assert.True(ContainsType(publisher.Notifications,
                    typeof(DomainEvent<UpdateRequest>)));
            }
            else
            {
                Assert.NotEmpty(notValidNotifications);
                Assert.False(ContainsType(publisher.Notifications,
                    typeof(DomainEvent<UpdateRequest>)));
            }
        }

        private static bool ContainsType<T>(IEnumerable<T> collection, Type type)
        {
            return collection.Any(i => i.GetType() == type);
        }

        protected override void AddServices(IServiceCollection services,
            IConfiguration configuration)
        {
            Services.ConfigureTemplateServices(configuration);
        }
    }
}