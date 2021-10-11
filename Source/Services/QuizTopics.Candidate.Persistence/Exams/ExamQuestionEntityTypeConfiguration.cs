using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizTopics.Candidate.Domain.ExamsAggregate;

namespace QuizTopics.Candidate.Persistence.Exams
{
    public class ExamQuestionEntityTypeConfiguration : IEntityTypeConfiguration<ExamQuestion>
    {
        public void Configure(EntityTypeBuilder<ExamQuestion> builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.OwnsOne(x => x.Level, y => y.Property(z => z.Id)
                .HasColumnName("Level")
                .IsRequired());
        }
    }
}