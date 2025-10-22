namespace Pollify.Domain.DTOs;

public class CreateSurveyDto
{
    public int UserId { get; set; } 
    public string SurveyTitle { get; set; }
    public List<CreateQuestionDto> QuestionDtos { get; set; }
}