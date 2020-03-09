using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Application.Models.Result;
using Application.Repositories;

using FluentValidation.Results;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            // Arange
            IMediator mediator = ServiceProvider.GetService<IMediator>();

            MockEventPublisher publisher = new MockEventPublisher(mediator);
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>
                {
                    {"1", new Domain.Sample.Sample("1", "1")}
                });
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            MockSampleAdapter adapter = new MockSampleAdapter();

            UpdateHandler handler = new UpdateHandler(publisher, repository, uow.Object, adapter);

            UpdateRequest command = new UpdateRequest(id, description);

            // Act
            Result result = await handler.Handle(command, new CancellationToken());

            // Asert
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notif => !notif.IsValid).ToList();
            if (expected)
            {
                Assert.Empty(notValidNotifications);
                Assert.True(ContainsType(publisher.Notifications, typeof(SampleUpdated)));
            }
            else
            {
                Assert.NotEmpty(notValidNotifications);
                Assert.False(ContainsType(publisher.Notifications, typeof(SampleUpdated)));
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