using System.Collections.Generic;
using System.Linq;
using System.Threading;

using FluentValidation.Results;

using Kit.Application.Models.Responses;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Commands.Sample;
using Template.Application.Commands.Sample.Find;
using Template.Bootstrap;
using Template.Infra.Repositories;

using Test;
using Test.Mocks;

using Xunit;

namespace Template.Application.Tests.Sample.Find
{
    public class FindHandlerTests : IntegrationTestBase
    {
        [Theory]
        [InlineData(null, null, 3, 4)]
        [InlineData("", "", 3, 4)]
        [InlineData("1", "", 1, 1)]
        public async void FindHandler_Should_Work(string id, string description, int expected,
                                                  int count)
        {
            // Given
            IMediator mediator = ServiceProvider.GetService<IMediator>();

            MockEventPublisher publisher = new MockEventPublisher(mediator);
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>
                {
                    {"1", new Domain.Sample.Sample("1", "1")},
                    {"2", new Domain.Sample.Sample("2", "2")},
                    {"3", new Domain.Sample.Sample("3", "3")},
                    {"4", new Domain.Sample.Sample("4", "4")}
                });

            FindHandler handler = new FindHandler(repository, publisher);

            FindRequest command = new FindRequest(id, description, 1, 3);

            // When
            QueryResult<SampleDto> result = await handler.Handle(command, new CancellationToken());

            // Then
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notif => !notif.IsValid).ToList();

            Assert.Empty(notValidNotifications);
            Assert.Equal(expected, result.Items.Count());
            Assert.Equal(count, result.Count);
        }

        protected override void AddServices(IServiceCollection services,
                                            IConfiguration configuration)
        {
            Services.ConfigureTemplateServices(configuration);
        }
    }
}