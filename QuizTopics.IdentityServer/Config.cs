using System.Collections.Generic;
using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace QuizTopics.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new("candidateapi", "Generic access")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new()
                {
                    ClientId = "candidateclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedCorsOrigins = { "https://localhost:5005" },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RedirectUris = { "https://localhost:5005/authentication/login-callback" },
                    PostLogoutRedirectUris = { "https://localhost:5005/authentication/logout-callback" },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "candidateapi"
                    },
                    Enabled = true
                },
                new()
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "candidateapi" }
                },
                new()
                {
                    ClientId = "swaggerui",
                    ClientName = "Swagger UI for candidate api",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"https://localhost:5003/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:5003"},
                    AllowedScopes = {"candidateapi"}
                }
            };
    }
}