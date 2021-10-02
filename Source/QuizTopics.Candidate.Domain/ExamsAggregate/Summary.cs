using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public class Summary
    {
        private const int PercentageToPass = 70;

        private readonly Exam exam;

        public Summary(Exam exam)
        {
            this.exam = exam ?? throw new ArgumentNullException(nameof(exam));
        }

        public string QuizName => this.exam.QuizName;

        public string Candidate => this.exam.Candidate;

        public IReadOnlyList<ExamQuestion> CorrectExamQuestions =>
            this.exam.QuestionsCollection
                .Where(x => x.Answered && x.Answers.Any(y => y.Selected && y.IsCorrect))
                .ToList();

        public IReadOnlyList<ExamQuestion> WrongExamQuestions => 
            this.exam.QuestionsCollection
                .Where(x => !x.Answered || !x.Answers.Any(y => y.Selected && y.IsCorrect))
                .ToList();

        public bool IsExamPassed
        {
            get
            {
                var correctQuestionsCount = this.CorrectExamQuestions.Count;
                var percentComplete = (int)Math.Round((double)(100 * correctQuestionsCount) / this.exam.QuestionsCollection.Count);

                return percentComplete >= PercentageToPass;
            }
        }
    }
}