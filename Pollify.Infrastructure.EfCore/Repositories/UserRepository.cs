using Microsoft.EntityFrameworkCore;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.UserAgg;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class UserRepository(AppDbContext appDbContext) : IUserRepository
{
    public void Create(User user)
    {
        appDbContext.Add(user);
    }

    public int Delete(int id)
    {
        return appDbContext.Users.Where(u => u.Id == id).ExecuteDelete();
    }

    public bool IsUsernameExist(string username)
    {
        return appDbContext.Users.Any(u => u.UserName == username);
    }

    public bool IsUserExist(string username, string password)
    {
        return appDbContext.Users.Any(u => u.UserName == username && u.HashedPassword == password);
    }

    public UserDto? GetByUserName(string username)
    {
        return appDbContext.Users.Where(u => u.UserName == username)
            .Select(u => new UserDto()
            {
                Id = u.Id,
                Name = $"{u.FirstName} {u.LastName}",
                UserName = u.UserName,
                Role = u.Role
            }).FirstOrDefault();
    }

    public int UpdatePassword(int userId, string newPassword)
    {
        return appDbContext.Users.Where(u => u.Id == userId)
            .ExecuteUpdate(setter => setter
                .SetProperty(u => u.HashedPassword, newPassword));
    }

    public void Save()
    {
        appDbContext.SaveChanges();
    }
}