using System;
using System.Collections.Generic;
using System.Linq;
using QuizTopics.Candidate.Application.Quizzes.Queries;
using QuizTopics.Candidate.Domain.QuizzesAggregate;
using QuizTopics.Models;

namespace QuizTopics.Candidate.API.Quizzes.Many
{
    public static class GetQuizzesMapper
    {
        public static IEnumerable<QuizModel> AsModelCollection(this IEnumerable<QuizDto> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.Select(AsModel);
        }

        private static QuizModel AsModel(this QuizDto source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return new QuizModel(source.Id, source.Name, source.Category);
        }
    }
}