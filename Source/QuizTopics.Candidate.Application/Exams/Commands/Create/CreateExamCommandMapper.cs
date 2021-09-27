using System;
using QuizTopics.Candidate.Domain.Exams;

namespace QuizTopics.Candidate.Application.Exams.Commands.Create
{
    public static class CreateExamCommandMapper
    {
        public static ExamDto AsExamDto(this Exam exam)
        {
            if (exam == null)
            {
                throw new ArgumentNullException(nameof(exam));
            }

            return new ExamDto(exam.Id, exam.QuizName, exam.QuestionsCollection.Count);
        }
    }
}