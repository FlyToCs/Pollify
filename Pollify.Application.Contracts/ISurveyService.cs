using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface ISurveyService
{
    void Create(CreateSurveyDto createSurveyDto);
    int Delete(int surveyId);
    List<SurveyDto> GetAll();
    ResultSurveyDto ResultSurvey(int surveyId);
    bool IsVotedBefore(int surveyId, int userId);

}