using Pollify.Domain.Common;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Domain.Entities.UserAgg;

public class User : BaseEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; private set; }
    public string HashedPassword { get; private set; }
    public UserRoleEnum Role { get; private set; } 
    public List<Vote> Votes { get; set; } = [];
    public List<Survey> Surveys { get; set; } = [];

    public void ChangeName(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
        UpdateTimestamp();
    }

    public void ChangePassword(string newHashedPassword)
    {
        HashedPassword = newHashedPassword;
        UpdateTimestamp();
    }

    public User(string firstName, string lastName, string userName, string hashedPassword)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        HashedPassword = hashedPassword;
        Role = UserRoleEnum.Member;

    }
    private User()
    {

    }

}