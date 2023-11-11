using System.Collections.Generic;
using System.Security.Claims;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;



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
                new Client()
                {
                    ClientId = "angular",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:4200" },
                    PostLogoutRedirectUris = { "http://localhost:4200" },
                    AllowedCorsOrigins = { "http://localhost:4200" },

                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        "secretApi",
                    },

                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                },
                new Client()
                {
                    ClientId = "postman",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = { "https://oauth.pstmn.io/v1/callback" },
                    AllowedScopes =
                    {
                        "secretApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    RequireConsent = false
                },
                new Client()
                {
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedScopes = {  "secretApi",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile },
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
