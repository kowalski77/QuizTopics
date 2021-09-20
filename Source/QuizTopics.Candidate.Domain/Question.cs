using System.Collections.Generic;
using System.Linq;

namespace QuizTopics.Candidate.Domain
{
    public class Question
    {
        private readonly List<Answer> answerCollection;

        public Question(string text, string tag, Difficulty difficulty, IEnumerable<Answer> answers)
        {
            this.Text = text;
            this.Tag = tag;
            this.Difficulty = difficulty;
            this.answerCollection = answers.ToList();
        }

        public string Text { get; private set; }

        public string Tag { get; private set; }

        public Difficulty Difficulty { get; private set; }

        public IEnumerable<Answer> Answers => this.answerCollection;
    }
}