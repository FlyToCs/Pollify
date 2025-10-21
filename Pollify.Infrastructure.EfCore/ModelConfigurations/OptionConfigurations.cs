using System.Collections.Immutable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Infrastructure.EfCore.ModelConfigurations;

public class OptionConfigurations : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Text).HasMaxLength(200);

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(o => o.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(o => o.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(o => o.Votes)
            .WithOne(v => v.Option)
            .HasForeignKey(v => v.Option)
            .OnDelete(DeleteBehavior.Cascade);
    }
}