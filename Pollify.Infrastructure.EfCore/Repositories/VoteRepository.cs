using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Domain.Repository_Contracts;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class VoteRepository(AppDbContext context) : IVoteRepository
{
    public void Create(int userId, int optionId)
    {
        var vote = new Vote()
        {
            UserId = userId,
            OptionId = optionId
        };
        context.Add(vote);
        context.SaveChanges();
    }
}