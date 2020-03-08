using System.IO;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Test
{
    public abstract class IntegrationTestBase
    {
        protected readonly ServiceProvider   ServiceProvider;
        protected readonly IServiceCollection Services;

        protected IntegrationTestBase()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            IConfigurationRoot configuration = builder.Build();

            Services = new ServiceCollection();
            Services.AddLogging();

            AddServices(Services, configuration);

            ServiceProvider = Services.BuildServiceProvider();
        }

        protected abstract void AddServices(IServiceCollection services,
            IConfiguration configuration);
    }
}
