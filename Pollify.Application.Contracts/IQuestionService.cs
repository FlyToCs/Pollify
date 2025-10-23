using Pollify.Domain.DTOs;

namespace Pollify.Application.Contracts;

public interface IQuestionService
{
    void Create(string question);
    List<QuestionWithOptionsDto> GetQuestions(int surveyId);


}