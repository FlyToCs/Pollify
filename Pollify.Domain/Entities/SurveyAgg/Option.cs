using Pollify.Domain.Common;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Option(string text) : BaseEntity<int>
{
    public string Text { get; set; } = text;
    public Question Question { get; set; }
    public int QuestionId { get; set; }
}