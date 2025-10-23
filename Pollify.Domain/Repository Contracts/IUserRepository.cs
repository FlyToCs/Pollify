using Pollify.Domain.DTOs;

namespace Pollify.Domain.Entities.UserAgg;

public interface IUserRepository
{
    void Create(User user);
    int Delete(int id);
    bool IsUsernameExist(string username);
    bool IsUserExist(string username, string password);
    UserDto? GetByUserName(string username);
    UserLoginDto? LoginGetByUserName(string username);
    int UpdatePassword(int userId, string newPassword);
    void Save();

}