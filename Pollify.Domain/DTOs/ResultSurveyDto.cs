namespace Pollify.Domain.DTOs;

public class ResultSurveyDto
{
    public int TotalParticipantCount { get; set; } 
    public List<SurveyParticipantsDto> Participants { get; set; }
    public List<QuestionResultDto> QuestionsResult { get; set; } 
}