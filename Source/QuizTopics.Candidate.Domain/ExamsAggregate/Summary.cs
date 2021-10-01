using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizTopics.Candidate.Domain.ExamsAggregate
{
    public class Summary
    {
        private const int PercentageToPass = 70;

        private readonly IReadOnlyList<ExamQuestion> questionsCollection;

        public Summary(IReadOnlyList<ExamQuestion> questionsCollection)
        {
            this.questionsCollection = questionsCollection ?? 
                                       throw new ArgumentNullException(nameof(questionsCollection));
        }

        public IReadOnlyList<ExamQuestion> CorrectExamQuestions =>
            this.questionsCollection
                .Where(x => x.Answered && x.Answers.Any(y => y.Selected && y.IsCorrect))
                .ToList();

        public IReadOnlyList<ExamQuestion> WrongExamQuestions => 
            this.questionsCollection
                .Where(x => !x.Answered || !x.Answers.Any(y => y.Selected && y.IsCorrect))
                .ToList();

        public bool IsExamPassed
        {
            get
            {
                var correctQuestionsCount = this.CorrectExamQuestions.Count;
                var percentComplete = (int)Math.Round((double)(100 * correctQuestionsCount) / this.questionsCollection.Count);

                return percentComplete >= PercentageToPass;
            }
        }
    }
}