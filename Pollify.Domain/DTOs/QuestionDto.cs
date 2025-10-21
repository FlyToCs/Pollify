namespace Pollify.Domain.DTOs;

public class QuestionDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public List<string> Options { get; set; }
}