using Microsoft.EntityFrameworkCore;
using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Infrastructure.EfCore.persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Survey> Surveys { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Option> Options { get; set; }
    public DbSet<Vote> Votes { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Hotel-MS;User ID=sa; Password=123456;Trust Server Certificate=True");
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}