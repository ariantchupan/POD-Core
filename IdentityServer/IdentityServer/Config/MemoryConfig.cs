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
                new IdentityResources.Profile(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string>{"role"}
                }
            };
        public static IEnumerable<Client> Clients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = {new Secret("SuperSecretPassword".Sha256())},

                    AllowedGrantTypes = GrantTypes.Code,

                    RedirectUris = {"https://localhost:5001/signin-oidc"},
                    FrontChannelLogoutUri = "https://localhost:5001/signout-oidc",
                    PostLogoutRedirectUris = {"https://localhost:5001/signout-callback-oidc"},

                    AllowOfflineAccess = true,
                    AllowedScopes = {"openid", "profile", "weatherapi.read"},
                    RequirePkce = true,
                    RequireConsent = true,
                    AllowPlainTextPkce = false
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
