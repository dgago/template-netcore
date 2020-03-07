using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Application.Models.Result;

using FluentValidation.Results;

using Microsoft.Extensions.DependencyInjection;

using Template.Application.Commands.Sample;
using Template.Application.Commands.Sample.Find;
using Template.Application.Tests.Sample.Mocks;
using Template.Bootstrap;

using Test;

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
            // Arange
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>()
                {
                    {"1", new Domain.Sample.Sample("1", "1")},
                    {"2", new Domain.Sample.Sample("2", "2")},
                    {"3", new Domain.Sample.Sample("3", "3")},
                    {"4", new Domain.Sample.Sample("4", "4")},
                });

            FindHandler handler = new FindHandler(repository);

            FindRequest command = new FindRequest(id, description, 1, 3);

            // Act
            QueryResult<SampleDto> result = await handler.Handle(command, new CancellationToken());

            // Asert
            List<ValidationResult> notValidNotifications =
                result.Notifications.Where(notif => !notif.IsValid).ToList();

            Assert.Empty(notValidNotifications);
            Assert.Equal(expected, result.Items.Count());
            Assert.Equal(count, result.Count);
        }

        protected override void AddServices(ServiceCollection services)
        {
            Services.AddTemplate();
        }
    }
}