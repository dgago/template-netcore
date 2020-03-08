using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Application.Models.Result;

using FluentValidation.Results;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Commands.Sample.GetById;
using Template.Application.Tests.Sample.Mocks;
using Template.Bootstrap;

using Test;

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
            // Arange
            MockSampleRepository repository = new MockSampleRepository(
                new Dictionary<string, Domain.Sample.Sample>
                {
                    {"1", new Domain.Sample.Sample("1", "1")}
                });

            GetByIdHandler handler = new GetByIdHandler(repository);

            GetByIdRequest command = new GetByIdRequest(id);

            // Act
            Result result = await handler.Handle(command, new CancellationToken());

            // Asert
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