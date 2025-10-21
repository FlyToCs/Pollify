using Pollify.Domain.Common;
using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Survey : BaseEntity<int>
{
    public string Name { get; set; }
    public List<Question> Questions { get; set; } = [];
    public User User { get; set; }
    public int  UserId { get; set; }
}