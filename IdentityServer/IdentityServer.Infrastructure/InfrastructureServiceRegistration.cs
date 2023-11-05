using Duende.IdentityServer.Test;
using IdentityServer.Application.Contracts.Persistence;
using IdentityServer.Infrastructure.Persistence;
using IdentityServer.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityServer.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILocalUserService, LocalUserService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseSqlite(
                    configuration.GetConnectionString("IdentityDBConnectionString"));
            });
            return services;
        }
    }
}
