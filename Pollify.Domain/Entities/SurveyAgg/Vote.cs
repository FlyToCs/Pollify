using Pollify.Domain.Common;
using Pollify.Domain.Entities.UserAgg;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Vote : BaseEntity<int>
{
    public User User { get; set; }
    public int UserId { get; set; }
    public Option Option { get; set; }
    public int OptionId { get; set; }
}