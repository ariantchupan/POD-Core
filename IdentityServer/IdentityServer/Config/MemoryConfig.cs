using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Config
{
    public class MemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId= "first-client",
                    ClientSecrets=new []{new Secret("atchupan".ToSha512())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes={IdentityServerConstants.StandardScopes.OpenId}
                }
            };
        public static List<TestUser> TestUsers() =>
            new List<TestUser>
            {
                new TestUser
                {
                    SubjectId="abc1",
                    Username="arian",
                    Password="arian",
                    Claims= new List<Claim>
                    {
                        new Claim("given_name","frank"),
                        new Claim("family_name","ozz")
                    }
                },
                new TestUser
                {
                    SubjectId="abc2",
                    Username="seros",
                    Password="arian",
                    Claims= new List<Claim>
                    {
                        new Claim("given_name","seros"),
                        new Claim("family_name","ozz")
                    }
                }
            };
    }
}
