using Microsoft.AspNetCore.Components;

namespace QuizTopics.Candidate.Wasm.Utils
{
    public class ConditionBase : ComponentBase
    {
        [Parameter]
        public bool Evaluation { get; set; }

        [Parameter]
        public RenderFragment Match { get; set; }

        [Parameter]
        public RenderFragment NotMatch { get; set; }
    }
}