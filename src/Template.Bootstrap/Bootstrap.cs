using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Template.Application.Adapters;
using Template.Application.Commands.Sample.Insert;
using Template.Application.Repositories;
using Template.Application.Services;
using Template.Infra.Adapters;
using Template.Infra.Repositories;
using Template.Infra.Services;

namespace Template.Bootstrap
{
    public static class Bootstrap
    {
        public static IServiceCollection AddTemplate(this ServiceCollection services)
        {
            services.AddSingleton<ISampleRepository, MockSampleRepository>();
            services.AddSingleton<ISampleAdapter, MockSampleAdapter>();
            services.AddSingleton<ISampleService, MockSampleService>();
            services.AddMediatR(typeof(InsertHandler).Assembly);

            return services;
        }
    }
}