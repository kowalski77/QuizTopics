using System;
using System.Collections.Generic;

namespace QuizCreatedOrUpdatedService.FunctionApp
{
    public class QuizDesignerEvents
    {
        public sealed record QuizCreated(Guid Id, string Name, string Exam, IEnumerable<ExamQuestion> ExamQuestionCollection);

        public sealed record ExamQuestion(string Text, IEnumerable<ExamAnswer> ExamAnswerCollection);

        public sealed record ExamAnswer(string Text, bool IsCorrect);
    }
}