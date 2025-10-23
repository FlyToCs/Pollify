using Pollify.Domain.DTOs;

namespace Pollify.Domain.Repository_Contracts;

public interface IQuestionRepository
{
    List<QuestionWithOptionsDto> GetQuestions(int surveyId);
}