namespace QuizCreatedOrUpdatedService.FunctionApp
{
    #nullable disable
    public class FunctionOptions
    {
        public string PostEndPoint { get; set; }

        public string IdentityServerEndPoint { get; set; }

        public TokenCredentials TokenCredentials { get; set; }
    }

    public class TokenCredentials
    {
        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }
    }
    #nullable enable
}