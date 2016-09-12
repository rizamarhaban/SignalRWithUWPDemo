using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCoreSignalR
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR(
                options => options.Hubs.EnableDetailedErrors = true);
        }

        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyOrigin();
                cors.AllowAnyMethod();
            });

            app.UseWebSockets();
            app.UseSignalR();

        }
    }
}
