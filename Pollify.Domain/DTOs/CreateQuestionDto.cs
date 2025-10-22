namespace Pollify.Domain.DTOs;

public class CreateQuestionDto
{
    public string Text { get; set; }
    public List<string> Options { get; set; } = [];
}