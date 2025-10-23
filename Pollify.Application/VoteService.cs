using Pollify.Application.Contracts;
using Pollify.Domain.Repository_Contracts;

namespace Pollify.Application;

public class VoteService(IVoteRepository voteRepository) : IVoteService
{
    public void Create(int userId, int optionId)
    {
        voteRepository.Create(userId, optionId);
    }
}