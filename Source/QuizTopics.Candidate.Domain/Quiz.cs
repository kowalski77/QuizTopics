namespace QuizTopics.Candidate.Domain
{
    public class Quiz
    {
        public Quiz(string name, string examName)
        {
            this.Name = name;
            this.ExamName = examName;
        }

        public string Name { get; }

        public string ExamName { get; }
    }
}