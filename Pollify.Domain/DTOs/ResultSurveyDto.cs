namespace Pollify.Domain.DTOs;

public class ResultSurveyDto
{
    public List<SurveyParticipantsDto> Participants { get; set; }
    public List<QuestionResultDto> OptionsResult { get; set; }
}