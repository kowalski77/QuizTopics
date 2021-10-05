using System;
using System.Collections.Generic;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Application.Exams
{
    public sealed record ExamDto(Guid Id, string Name, string Candidate);

    public sealed record CreateExamDto(Guid Id, string UserEmail, Guid QuizId);

    public sealed record ExamQuestionDto(Guid Id, string Text, Difficulty Difficulty, bool Answered, IEnumerable<ExamAnswerDto> ExamAnswersCollection)
    {
        private ExamQuestionDto() : this(Guid.Empty, string.Empty, Difficulty.Unknown, false, new List<ExamAnswerDto>())
        {
        }

        public static ExamQuestionDto None => new();
    }

    public sealed record ExamAnswerDto(Guid Id, string Text, bool Selected);

    public sealed record SummaryDto(IEnumerable<ExamQuestionDto> CorrectQuestionsCollection, IEnumerable<ExamQuestionDto> WrongQuestionsCollection, bool Passed);
}