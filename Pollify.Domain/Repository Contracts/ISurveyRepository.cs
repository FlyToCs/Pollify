using Pollify.Domain.DTOs;

namespace Pollify.Domain.Entities.SurveyAgg;

public interface ISurveyRepository
{
    int Create(string name,int userId);
    bool IsSurveyTitleExist(string title);
    bool SurveyHasParticipant(int surveyId);
    int Delete(int surveyId);
    List<SurveyDto> GetAll();
    ResultSurveyDto ResultSurvey(int surveyId);
    bool IsVotedBefore(int surveyId, int userId);
    void Save();
}