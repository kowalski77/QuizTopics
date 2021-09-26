using System;
using System.Collections.Generic;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Application.Exams.Queries
{
    public sealed record ExamQuestionDto(Guid Id, string Text, Difficulty Difficulty, bool Answered, IEnumerable<ExamAnswerDto> ExamAnswersCollection);

    public sealed record ExamAnswerDto(Guid Id, string Text);
}