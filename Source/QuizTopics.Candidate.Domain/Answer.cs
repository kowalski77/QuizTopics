namespace QuizTopics.Candidate.Domain
{
    public class Answer
    {
        public Answer(string text, bool isCorrect)
        {
            this.Text = text;
            this.IsCorrect = isCorrect;
        }

        public string Text { get; private set; }

        public bool IsCorrect { get; private set; }
    }
}