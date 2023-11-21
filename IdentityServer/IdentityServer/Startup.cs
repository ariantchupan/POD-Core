using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using IdentityServer.Infrastructure;
using IdentityServer.Application;
using IdentityServer.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer.Config;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Extensions;
using Microsoft.AspNetCore.Cors.Infrastructure;

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

			string configurationStoreCS = _configuration.GetConnectionString("configurationStoreCS");
			string operationalStoreCS = _configuration.GetConnectionString("operationalStoreCS");

			services.AddMassTransit(config =>
			{
				config.UsingRabbitMq((ctx, cfg) =>
				{
					cfg.Host(_configuration["EventBusSettings:HostAddress"]);

				});
			});


			var corsBuilder = new CorsPolicyBuilder();
			corsBuilder.AllowAnyHeader();
			corsBuilder.AllowAnyMethod();
			corsBuilder.AllowAnyOrigin(); // For anyone access.
			//corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
			corsBuilder.AllowCredentials();

			services.AddCors(options =>
			{
				options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
			});
			services.AddIdentityServer(options =>
				{
					options.Events.RaiseErrorEvents = true;
					options.Events.RaiseFailureEvents = true;
				})
				.AddExtensionGrantValidator<PhoneNumberTokenGrantValidator>()
				.AddAspNetIdentity<IdentityUser>()

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
				.AddDeveloperSigningCredential();


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

			app.UseCors("SiteCorsPolicy");

			app.UseRouting();
			app.UseIdentityServer();


			// app.UseHttpsRedirection();
	


			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}


	}
}
