using Pollify.Domain.Common;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Domain.Entities.UserAgg;

public class User(string firstName, string lastName, string userName, string hashedPassword)
    : BaseEntity<int>
{
    public string FirstName { get; set; } = firstName;
    public string LastName { get; set; } = lastName;
    public string UserName { get; private set; } = userName;
    public string HashedPassword { get; private set; } = hashedPassword;
    public UserRoleEnum Role { get; private set; } = UserRoleEnum.Member;
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

}