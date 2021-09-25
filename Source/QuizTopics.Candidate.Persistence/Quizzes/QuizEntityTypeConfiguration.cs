using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Persistence.Quizzes
{
    public class QuizEntityTypeConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable(nameof(Quiz));

            builder.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(x => x.Category)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}