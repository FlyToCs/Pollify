using Pollify.Domain.DTOs;

namespace Pollify.Domain.Entities.SurveyAgg;

public interface ISurveyRepository
{
    void Create(CreateSurveyDto createSurveyDto);
    bool IsSurveyTitleExist(string title);
    bool SurveyHasParticipant(int surveyId);
    int Delete(int surveyId);
    List<SurveyDto> GetAll();
    ResultSurveyDto ResultSurvey(int surveyId);
    void Save();
}