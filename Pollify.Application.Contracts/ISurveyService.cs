using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface ISurveyService
{
    void Create(string title);
    int Delete(int surveyId);
    List<SurveyDto> GetAll();
    public List<ResultSurveyDto> ResultSurvey { get; set; }

}