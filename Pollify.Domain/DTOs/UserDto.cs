using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Domain.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string UserName { get; set; }
    public UserRoleEnum Role { get; set; }
}