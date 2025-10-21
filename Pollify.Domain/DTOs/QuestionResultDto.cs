namespace Pollify.Domain.DTOs;

public class QuestionResultDto
{
    public string QuestionTitle { get; set; }
    public List<OptionResultDto> OptionsResults { get; set; }
}