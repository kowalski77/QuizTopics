using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizTopics.Candidate.Application.Quizzes.Commands.Queries;

namespace QuizTopics.Candidate.Persistence
{
    public sealed class QuizProvider : IQuizProvider
    {
        private readonly QuizTopicsContext context;

        public QuizProvider(QuizTopicsContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<QuizDto>> GetQuizCollectionAsync(CancellationToken cancellationToken = default)
        {
            var quizCollection = await this.context.Quizzes!
                .Select(x => new QuizDto(x.Id, x.Name, x.Category)).ToListAsync(cancellationToken);

            return quizCollection;
        }
    }
}