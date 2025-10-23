using Pollify.Domain.DTOs;

namespace Pollify.Domain.Repository_Contracts;

public interface IQuestionRepository
{
    int Create(string title, int surveyId);
    List<QuestionWithOptionsDto> GetQuestions(int surveyId);
}