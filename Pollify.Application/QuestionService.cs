using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Repository_Contracts;

namespace Pollify.Application;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public void Create(string question)
    {
        throw new NotImplementedException();
    }

    public List<QuestionWithOptionsDto> GetQuestions(int surveyId)
    {
        return questionRepository.GetQuestions(surveyId);
    }
}