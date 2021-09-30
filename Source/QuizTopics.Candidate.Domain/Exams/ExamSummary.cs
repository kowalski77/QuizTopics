using System;
using System.Collections.Generic;
using System.Linq;
using QuizDesigner.Common.DomainDriven;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class ExamSummary : Entity
    {
        private List<ExamQuestion> correctExamQuestionsCollection = new();
        private List<ExamQuestion> wrongExamQuestionsCollection = new();

        private readonly List<ExamQuestion> examQuestionsCollection;

        public ExamSummary(IEnumerable<ExamQuestion> examQuestionsCollection)
        {
            this.examQuestionsCollection = examQuestionsCollection.ToList() ??
                                           throw new ArgumentNullException(nameof(examQuestionsCollection));
        }

        public IReadOnlyList<ExamQuestion> CorrectExamQuestionsCollection => this.correctExamQuestionsCollection;

        public IReadOnlyList<ExamQuestion> WrongExamQuestionsCollection => this.wrongExamQuestionsCollection;

        public int CorrectAnswers => this.correctExamQuestionsCollection.Count;

        public int WrongAnswers => this.wrongExamQuestionsCollection.Count;

        public void Calculate()
        {
            this.correctExamQuestionsCollection = this.examQuestionsCollection
                .Where(x => x.Answered && x.Answers.Any(y => y.IsCorrect && y.Selected))
                .ToList();

            this.wrongExamQuestionsCollection = this.examQuestionsCollection
                .Where(x => !x.Answered || x.Answers.Any(y => y.IsCorrect && !y.Selected))
                .ToList();
        }
    }
}