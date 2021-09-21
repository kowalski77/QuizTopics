using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace QuizTopics.Candidate.Wasm.MessageHandlers
{
    public class QuizTopicsApiAuthorizationHandler : AuthorizationMessageHandler
    {
        public QuizTopicsApiAuthorizationHandler(IAccessTokenProvider provider, NavigationManager navigation)
            : base(provider, navigation)
        {
            this.ConfigureHandler(new[] { "https://localhost:5003/" });
        }
    }
}