using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface IQuestionService
{
    int Create(string question, int surveyId);
    List<QuestionWithOptionsDto> GetQuestions(int surveyId);


}