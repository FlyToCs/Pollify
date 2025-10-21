using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Infrastructure.EfCore.ModelConfigurations;

public class SurveyConfigurations : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).HasMaxLength(250);

        builder.Property(s => s.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(s => s.User)
            .WithMany(u => u.Surveys)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.Questions)
            .WithOne(q => q.Survey)
            .HasForeignKey(q => q.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        
    }
}