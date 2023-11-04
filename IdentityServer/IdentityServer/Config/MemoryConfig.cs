using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;



namespace IdentityServer.Config
{
    public class MemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {

            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
               
            };
        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
               
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
