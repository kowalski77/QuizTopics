using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizTopics.Candidate.Domain.QuizzesAggregate;

namespace QuizTopics.Candidate.Persistence.Quizzes
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Property(x => x.Text)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.Tag)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasMany(x => x.Answers)
                .WithOne()
                .HasForeignKey("QuestionId");

            builder.OwnsOne(x => x.Level, y => y.Property(z => z.Id)
                .HasColumnName("Level")
                .IsRequired());
        }
    }
}