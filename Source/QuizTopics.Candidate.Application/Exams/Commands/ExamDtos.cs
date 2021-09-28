using System;
using System.Collections.Generic;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Application.Exams.Commands
{
    public sealed record ExamDto(Guid Id, string QuizName, int QuestionsNumber);

    public sealed record ExamQuestionDto(Guid Id, string Text, Difficulty Difficulty, bool Answered, IEnumerable<ExamAnswerDto> ExamAnswersCollection)
    {
        private ExamQuestionDto() : this(Guid.Empty, string.Empty, Difficulty.Unknown, false, new List<ExamAnswerDto>())
        {
        }

        public static ExamQuestionDto None => new();
    }

    public sealed record ExamAnswerDto(Guid Id, string Text);
}