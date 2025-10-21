using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface IUserService
{
    void Register(string firstName, string lastName, string userName, string password);
    UserDto Login(string userName, string password);
    void UpdatePassword(int userId, string password);
    int DeleteAccount(int userId);


}