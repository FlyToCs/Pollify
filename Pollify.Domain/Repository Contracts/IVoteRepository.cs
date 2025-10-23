namespace Pollify.Domain.Repository_Contracts;

public interface IVoteRepository
{
    void Create(int userId, int optionId);
}