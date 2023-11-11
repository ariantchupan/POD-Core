
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AspNetIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityDBConnectionString")
                    , b => b.MigrationsAssembly(("IdentityServer.Infrastructure")));

            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>();
      
            return services;
        }
    }
}
