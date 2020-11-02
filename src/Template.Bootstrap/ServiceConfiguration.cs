using Kit.Application.Handlers;
using Kit.Application.Repositories;

using MediatR;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Template.Application.Adapters;
using Template.Application.Commands.Sample.Insert;
using Template.Application.Repositories;
using Template.Application.Services;
using Template.Infra;
using Template.Infra.Adapters;
using Template.Infra.Repositories;
using Template.Infra.Services;

namespace Template.Bootstrap
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection ConfigureTemplateServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTemplateInfrastructure()
                .AddMediatR(typeof(InsertHandler).Assembly)
                .AddSingleton<IEventPublisher, MediatorEventPublisher>();

            return services;
        }

        private static IServiceCollection AddTemplateInfrastructure(
            this IServiceCollection services)
        {
            services.AddSingleton<ISampleRepository, MockSampleRepository>()
                .AddSingleton<ISampleAdapter, MockSampleAdapter>()
                .AddSingleton<ISampleService, MockSampleService>()
                .AddSingleton<IUnitOfWork, SampleUnitOfWork>();

            return services;
        }
    }
}