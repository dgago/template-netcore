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

using Moq;

using Template.Application.Commands.Sample;
using Template.Application.Commands.Sample.Insert;
using Template.Bootstrap;
using Template.Infra.Repositories;
using Template.Infra.Services;

using Test;
using Test.Mocks;

using Xunit;

namespace Template.Application.Tests.Sample.Insert
{
    public class InsertHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(null, null, false)]
        [InlineData("", "", false)]
        [InlineData(null, "", false)]
        [InlineData("", null, false)]
        [InlineData("1", "2", true)]
        public async void InsertHandler_Should_Work(string id, string description, bool expected)
        {
            // Given
            IMediator mediator = ServiceProvider.GetService<IMediator>();

            MockEventPublisher publisher = new MockEventPublisher(mediator);
            MockSampleRepository repository =
                new MockSampleRepository(new Dictionary<string, Domain.Sample.Sample>());
            Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
            MockSampleService service = new MockSampleService();

            InsertHandler handler = new InsertHandler(publisher, repository, uow.Object, service);

            SampleDto item = new SampleDto {Id = id, Description = description};
            InsertRequest command = new InsertRequest(item);

            // When
            EntityResult<SampleDto> result = await handler.Handle(command, new CancellationToken());

            // Then
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notification => !notification.IsValid).ToList();
            if (expected)
            {
                Assert.Empty(notValidNotifications);
                Assert.True(ContainsType(publisher.Notifications,
                    typeof(DomainEvent<InsertRequest>)));
            }
            else
            {
                Assert.NotEmpty(notValidNotifications);
                Assert.False(ContainsType(publisher.Notifications,
                    typeof(DomainEvent<InsertRequest>)));
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