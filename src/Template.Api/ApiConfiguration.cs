using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            return services;
        }

        public static IApplicationBuilder ConfigureMvc(this IApplicationBuilder app,
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            return app;
        }
    }
}