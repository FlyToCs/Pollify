using Pollify.Domain.Common;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Option(string text) : BaseEntity<int>
{
    public string Text { get; set; } 
    public Question Question { get; set; }
    public int QuestionId { get; set; }
    public List<Vote> Votes { get; set; }
    
}