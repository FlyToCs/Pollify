using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Application;

public class UserService(IUserRepository userRepository) : IUserService
{
    public void Register(string firstName, string lastName, string userName, string password)
    {
        if (firstName.Length<3 || lastName.Length<3)
            throw new Exception("FirstName or LastName can't be less than 3 characters");

        if (userName.Length<5)
            throw new Exception("username can't be less than 5 characters");

        if (userRepository.IsUsernameExist(userName))
            throw new Exception("the username is already taken");

        if (password.Length<6)
            throw new Exception("the password can't be less than 6 characters");
        
        userRepository.Create(new User(firstName, lastName, userName , password));
        userRepository.Save();

    }

    public UserDto Login(string userName, string password)
    {
        if (!userRepository.IsUserExist(userName, password))
            throw new Exception("the username or password is incorrect");

        return userRepository.GetByUserName(userName)!;

    }

    public void ChangePassword(int userId, string password)
    {
        userRepository.UpdatePassword(userId, password);
    }

    public int DeleteAccount(int userId)
    {
        return userRepository.Delete(userId);
    }
}