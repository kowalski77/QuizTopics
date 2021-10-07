using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace QuizTopics.Candidate.Wasm.Utils
{
    public class IterationBase<T> : ComponentBase
    {
        [Parameter]
        public IEnumerable<T> Items { get; set; }

        [Parameter]
        public RenderFragment<T> ChildContent { get; set; }
    }
}