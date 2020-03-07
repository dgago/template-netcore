using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public abstract class IntegrationTestBase
    {
        protected readonly ServiceProvider   ServiceProvider;
        protected readonly ServiceCollection Services;

        protected IntegrationTestBase()
        {
//            IConfigurationBuilder builder = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json", true, true)
//                .AddEnvironmentVariables();
//
//            IConfigurationRoot configuration = builder.Build();

            Services = new ServiceCollection();
            Services.AddLogging();

            AddServices(Services);

            ServiceProvider = Services.BuildServiceProvider();

//            IServiceScopeFactory scopeFactory =
//                this.ServiceProvider.GetService<IServiceScopeFactory>();
        }

        protected abstract void AddServices(ServiceCollection services);
    }
}