using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Abstractions;
using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Infrastructure.EfCore.ModelConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .HasMaxLength(100);

        builder.Property(u => u.UserName)
            .HasMaxLength(100).IsRequired(true);

        builder.HasIndex(u => u.UserName)
            .IsUnique();

        builder.Property(u => u.Role)
            .HasConversion<string>();

        builder.Property(u=>u.CreatedAt)
            .HasDefaultValueSql("GetUtcDate()")
            .ValueGeneratedOnAdd();

        builder.HasMany(u => u.Surveys)
            .WithOne(s => s.User)
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.Votes)
            .WithOne(v => v.User)
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}