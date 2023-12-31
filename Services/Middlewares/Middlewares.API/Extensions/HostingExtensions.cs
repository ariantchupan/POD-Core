﻿using EventBus.Messages.Common;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Middlewares.API.EventBusConsumer;
using Middlewares.Application;
using Middlewares.Infrastructure;

namespace Middlewares.API.Extensions
{
    public static class HostingExtensions
    {

        private static readonly IConfiguration _config;

        public static WebApplication ConfigureServices(this WebApplicationBuilder builder, IConfiguration _configuration)
        {

            builder.Services.AddControllers();
            builder.Services.AddCors();
            builder.Services.AddMassTransit(config =>
            {
                config.AddConsumer<SMSVerifyConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
	                cfg.AutoStart = true;
					cfg.Host(_configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstants.VerificationSMSQueue, c =>
                    {
                        c.ConfigureConsumer<SMSVerifyConsumer>(ctx);
                        
					});
                });
            });

            builder.Services.AddScoped<SMSVerifyConsumer>();
            builder.Services.AddAutoMapper(typeof(Program));


            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);

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

           // app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            return app;
        }
    }
}
