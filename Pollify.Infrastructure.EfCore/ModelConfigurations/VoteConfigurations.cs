using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Infrastructure.EfCore.ModelConfigurations;

public class VoteConfigurations : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(v => v.User)
            .WithMany(u => u.Votes)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(v => v.Option)
            .WithMany(o => o.Votes)
            .HasForeignKey(v => v.OptionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}