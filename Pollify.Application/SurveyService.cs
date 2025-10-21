using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;

namespace Pollify.Application;

public class SurveyService:ISurveyService
{
    public void Create(string title)
    {
        throw new NotImplementedException();
    }

    public int Delete(int surveyId)
    {
        throw new NotImplementedException();
    }

    public List<SurveyDto> GetAll()
    {
        throw new NotImplementedException();
    }

    public List<ResultSurveyDto> ResultSurvey { get; set; }
}