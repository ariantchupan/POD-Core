using Microsoft.Extensions.DependencyInjection;
using Middlewares.Application.Model.Kavenegar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Middlewares.Application.Contracts.Infrastructure;
using Middlewares.Infrastructure.KavenegarSMS;
using Microsoft.Extensions.Configuration;

namespace Middlewares.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            services.Configure<KavenegarSettings>(c => configuration.GetSection("KavehnegarSettings"));
            services.AddTransient<IKavenegarService, KavehnegarService>();

            return services;
        }
    }
}
