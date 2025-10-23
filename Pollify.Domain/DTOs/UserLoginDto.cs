using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Domain.DTOs;

public class UserLoginDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public string HashedPassword { get; set; }
    public UserRoleEnum Role { get; set; }
}