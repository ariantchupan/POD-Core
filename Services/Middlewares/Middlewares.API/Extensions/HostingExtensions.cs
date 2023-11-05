using Microsoft.Extensions.Configuration;
using Middlewares.Application;
using Middlewares.Infrastructure;

namespace Middlewares.API.Extensions
{
    public static  class HostingExtensions
    {
        public static class ConfigurationHelper
        {
            public static IConfiguration config;
            public static void Initialize(IConfiguration Configuration)
            {
                config = Configuration;
            }
        }
        public static WebApplication ConfigureServices(
            this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(ConfigurationHelper.config);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
