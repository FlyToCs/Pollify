using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Repository_Contracts;

namespace Pollify.Application;

public class QuestionService(IQuestionRepository questionRepository) : IQuestionService
{
    public int Create(string question, int surveyId)
    {
       return questionRepository.Create(question, surveyId);
    }

    public List<QuestionWithOptionsDto> GetQuestions(int surveyId)
    {
        return questionRepository.GetQuestions(surveyId);
    }
}