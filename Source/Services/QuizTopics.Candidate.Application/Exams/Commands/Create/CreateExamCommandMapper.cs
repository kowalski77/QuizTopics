using System;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public static class CreateExamCommandMapper
    {
        public static CreateExamDto AsExamDto(this Exam exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            return new CreateExamDto(exam.Id, exam.QuizName, exam.QuestionsCollection.Count);
        }
    }
}