namespace Pollify.Domain.Entities.UserAgg;

public interface IUserRepository
{
    void Create(User user);
    int Delete(int id);
    

}