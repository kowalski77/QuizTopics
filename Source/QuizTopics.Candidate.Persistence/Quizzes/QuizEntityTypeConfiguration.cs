using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizTopics.Candidate.Domain;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Persistence.Quizzes
{
    public class QuizEntityTypeConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.ExamName)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}