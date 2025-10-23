using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface ISurveyService
{
    int Create(string title, int userId);
    int Delete(int surveyId);
    List<SurveyDto> GetAll();
    ResultSurveyDto ResultSurvey(int surveyId);
    bool IsVotedBefore(int surveyId, int userId);

}