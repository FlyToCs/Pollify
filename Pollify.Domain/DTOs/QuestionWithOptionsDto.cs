namespace Pollify.Domain.DTOs;

public class QuestionWithOptionsDto
{
    public string Title { get; set; }
    public List<OptionDto> Options { get; set; }
}