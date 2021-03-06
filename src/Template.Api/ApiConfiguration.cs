using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Template.Api.Presenters;

namespace Template.Api
{
    public static class ApiConfiguration
    {
        public static IServiceCollection ConfigureApiServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<SamplesPresenter>();
            return services;
        }

        public static IServiceCollection ConfigureMvcServices(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app,
            IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());

            return app;
        }
    }
}