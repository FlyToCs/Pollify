using Pollify.Domain.Common;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Question : BaseEntity<int>
{
    public string Title { get; set; }
    public Survey Survey { get; set; }
    public int SurveyId { get; set; }
    public List<Option> Options { get; set; } = [];


}