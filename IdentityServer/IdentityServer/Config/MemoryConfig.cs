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
                //new IdentityResources.OpenId(),
                //new IdentityResources.Profile()
            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client()
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = { "secretApi" },
                }
            };
        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
                new ApiScope(){Name = "secretApi"},
            };
        public static IEnumerable<ApiResource> ApiResources() =>
            new List<ApiResource>
            {

            };
    }
}
