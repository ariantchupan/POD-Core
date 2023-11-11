using System;
using System.Linq;
using System.Security.Claims;
using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using IdentityServer.Config;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Extensions
{
    public static class HostingExtensions
    {
        public  static void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                var userMgr = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in MemoryConfig.Clients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in MemoryConfig.IdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiScopes.Any())
                {
                    foreach (var resource in MemoryConfig.ApiScopes())
                    {
                        context.ApiScopes.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
                var angella = userMgr.FindByNameAsync("angella").Result;
                if (angella == null)
                {
                    angella = new IdentityUser
                    {
                        UserName = "angella",
                        Email = "angella.freeman@email.com",
                        EmailConfirmed = true
                    };
                    var result = userMgr.CreateAsync(angella, "Pass123$").Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }

                    result =
                        userMgr.AddClaimsAsync(
                            angella,
                            new Claim[]
                            {
                                new Claim(JwtClaimTypes.Name, "Angella Freeman"),
                                new Claim(JwtClaimTypes.GivenName, "Angella"),
                                new Claim(JwtClaimTypes.FamilyName, "Freeman"),
                                new Claim(JwtClaimTypes.WebSite, "http://angellafreeman.com"),
                                new Claim("location", "somewhere")
                            }
                        ).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }
        }
    }
}
