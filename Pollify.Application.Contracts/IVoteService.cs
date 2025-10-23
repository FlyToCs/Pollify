

namespace Pollify.Application.Contracts
{
    public interface IVoteService
    {
        void Create(int userId, int optionId);
    }
}
