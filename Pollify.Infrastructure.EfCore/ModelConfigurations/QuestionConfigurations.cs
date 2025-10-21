using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Infrastructure.EfCore.ModelConfigurations;

public class QuestionConfigurations : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(q => q.Id);

        builder.Property(q => q.Title)
            .HasMaxLength(400).IsRequired(true);

        builder.Property(q => q.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(q => q.Survey)
            .WithMany(s => s.Questions)
            .HasForeignKey(q => q.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(q => q.Options)
            .WithOne(o => o.Question)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}