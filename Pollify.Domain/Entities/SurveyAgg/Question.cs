using Pollify.Domain.Common;

namespace Pollify.Domain.Entities.SurveyAgg;

public class Question : BaseEntity<int>
{
    public string Title { get; set; }
    public Survey Survey { get; set; }
    public int SurveyId { get; set; }
    public List<Option> Options { get; set; } = [];

    public Question(string title, List<string> optionTexts)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty.");
        
        if (optionTexts == null || optionTexts.Count != 4)
            throw new InvalidOperationException("A question must have exactly 4 options.");

        Title = title;

        foreach (var text in optionTexts)
        {
            Options.Add(new Option(text)); 
        }
    }
}