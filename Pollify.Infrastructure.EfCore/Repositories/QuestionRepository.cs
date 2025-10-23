using Microsoft.EntityFrameworkCore;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Domain.Repository_Contracts;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class QuestionRepository(AppDbContext context) : IQuestionRepository
{
    public int Create(string title, int surveyId)
    {
        var question = new Question()
        {
            Title = title,
            SurveyId = surveyId
        };
        context.Add(question);
        context.SaveChanges();
        return question.Id;
    }

    public List<QuestionWithOptionsDto> GetQuestions(int surveyId)
    {
        return context.Questions
            .Where(q => q.SurveyId == surveyId)
            .Include(q => q.Options)
            .Select(q => new QuestionWithOptionsDto
            {
                Title = q.Title,
                Options = q.Options
                    .Select(o => new OptionDto { Id = o.Id, Text = o.Text }).ToList()
            }).ToList();
    }

}