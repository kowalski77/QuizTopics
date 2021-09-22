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
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // where to redirect to after login
                    RedirectUris = { "https://localhost:5005/signin-oidc" },

                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "https://localhost:5005/signout-callback-oidc" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "candidateapi"
                    }
                }
            };
    }
}