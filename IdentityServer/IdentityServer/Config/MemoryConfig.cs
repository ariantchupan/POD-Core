using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using IdentityModel;
using IdentityServer.Constants;



namespace IdentityServer.Config
{
    public class MemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Phone()
            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "phone_number_authentication",
                    AllowedGrantTypes = new List<string> { AuthConstants.GrantType.PhoneNumberToken},
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "secretApi"
                    },
                    AllowOfflineAccess = true
                }
            };
        public static IEnumerable<ApiScope> ApiScopes() =>
            new List<ApiScope>
            {
               
            };
        public static IEnumerable<ApiResource> ApiResources() =>
            new List<ApiResource>
            {
                new ApiResource ("secretApi", "My Api") { UserClaims = { JwtClaimTypes.Role, JwtClaimTypes.PhoneNumber } }

            };

        public static List<TestUser> TestUsers() =>
            new List<TestUser>
            {
                new TestUser()
                {
                    Username = "test",
                    Password = "test",
                    SubjectId = "1"
                }
            };
    }
}
