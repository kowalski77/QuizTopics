using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizTopics.Candidate.Domain.Quizzes;

namespace QuizTopics.Candidate.Persistence.Quizzes
{
    public class AnswerEntityTypeConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();
        }
    }
}