using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Application;

public class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
{
    public void Create(CreateSurveyDto createSurveyDto)
    {
        if (surveyRepository.IsSurveyTitleExist(createSurveyDto.SurveyTitle))
            throw new Exception("you created a survey with this title before");
        
        surveyRepository.Create(createSurveyDto);
        surveyRepository.Save();
    }

    public int Delete(int surveyId)
    {
        if (!surveyRepository.SurveyHasParticipant(surveyId))
            throw new Exception("you can't delete this survey, this survey doesn't have any participants");
        
        return surveyRepository.Delete(surveyId);
    }

    public List<SurveyDto> GetAll()
    {
        return surveyRepository.GetAll();
    }

    public ResultSurveyDto ResultSurvey(int surveyId)
    {
        return surveyRepository.ResultSurvey(surveyId);
    }
}