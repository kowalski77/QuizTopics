using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace QuizTopics.Identity
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new("candidateapi", "QuizTopics.Candidate.API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new()
                {
                    ClientId = "candidateclient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes = { "candidateapi" }
                }
            };
        }
    }
}