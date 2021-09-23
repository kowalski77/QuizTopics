using Microsoft.Extensions.DependencyInjection;

namespace QuizDesigner.Common.DomainDriven
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository<TEntity, TContext>(this IServiceCollection services)
            where TEntity : class, IAggregateRoot
            where TContext : BaseContext
        {
            services.AddScoped<IRepository<TEntity>>(sp =>
                new BaseRepository<TEntity>(sp.GetRequiredService<TContext>()));

            return services;
        }
    }
}