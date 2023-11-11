using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using Duende.IdentityServer.Test;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using IdentityServer.Extensions;
using IdentityServer.Infrastructure;
using IdentityServer.Application;
using IdentityServer.Config;
using Microsoft.OpenApi.Models;

namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddApplicationServices();
            services.AddInfrastructureServices(_configuration);

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();


            //services.AddIdentityServer()
            //    .AddInMemoryClients(MemoryConfig.Clients())
            //    .AddInMemoryIdentityResources(MemoryConfig.IdentityResources())
            //    .AddInMemoryApiResources(MemoryConfig.ApiResources())
            //    .AddInMemoryApiScopes(MemoryConfig.ApiScopes())
            //    .AddTestUsers(MemoryConfig.TestUsers())
            //    .AddDeveloperSigningCredential();
            string configurationStoreCS = _configuration.GetConnectionString("configurationStoreCS");
            string operationalStoreCS = _configuration.GetConnectionString("operationalStoreCS");

            services.AddIdentityServer()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(configurationStoreCS,
                        sql => sql.MigrationsAssembly(typeof(Program).Assembly.FullName));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(operationalStoreCS,
                        sql => sql.MigrationsAssembly(typeof(Program).Assembly.FullName));
                })
                .AddTestUsers(MemoryConfig.TestUsers());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Ordering.API", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));
            }
            HostingExtensions.InitializeDatabase(app);
            app.UseIdentityServer();


            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
