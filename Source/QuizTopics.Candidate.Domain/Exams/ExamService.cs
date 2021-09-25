﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using QuizDesigner.Common.Results;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Domain.Exams
{
    public class ExamService : IExamService
    {
        private readonly IExamRepository examRepository;

        public ExamService(IExamRepository examRepository)
        {
            this.examRepository = examRepository ?? throw new ArgumentNullException(nameof(examRepository));
        }

        public async Task<Result<Exam>> CreateExamAsync(Quiz quiz, string userEmail, CancellationToken cancellationToken = default)
        {
            if (quiz == null)
            {
                throw new ArgumentNullException(nameof(quiz));
            }

            var maybeExam = await this.examRepository.GetExamByQuizAndCandidate(quiz.Name, userEmail, cancellationToken).ConfigureAwait(false);
            if (maybeExam.TryGetValue(out var existingExam))
            {
                return Result.Fail<Exam>(nameof(userEmail), $" this user: {userEmail} already toke the exam: {existingExam.QuizName}");
            }

            var exam = new Exam(quiz.Name, userEmail, GetExamQuestions(quiz.QuestionCollection));

            return Result.Ok(exam);
        }

        private static IEnumerable<ExamQuestion> GetExamQuestions(IEnumerable<Question> questions)
        {
            return questions.Select(x => 
                new ExamQuestion(x.Text, x.Tag, x.Difficulty, x.Answers.Select(y => 
                    new ExamAnswer(y.Text, y.IsCorrect))));
        }
    }
}