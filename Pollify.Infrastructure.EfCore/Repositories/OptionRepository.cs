using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Domain.Repository_Contracts;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class OptionRepository(AppDbContext context) : IOptionRepository
{
    public int Create(string text, int questionId)
    {
        var option = new Option()
        {
            Text = text,
            QuestionId = questionId
        };
        context.Add(option);
        context.SaveChanges();
        return questionId;
    }
}