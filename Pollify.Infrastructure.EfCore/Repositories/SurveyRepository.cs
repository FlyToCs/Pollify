using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class SurveyRepository(AppDbContext context) : ISurveyRepository
{
    public int Create(string name, int userId)
    {
        var survey = new Survey
        {
            Name = name,
            UserId = userId,
        };
        context.Surveys.Add(survey);
        context.SaveChanges();
        return survey.Id;
    }

    public bool IsSurveyTitleExist(string title)
    {
        return context.Surveys.Any(s => s.Name == title);
    }

    public bool SurveyHasParticipant(int surveyId)
    {
        return context.Votes
            .Any(v => v.Option.Question.SurveyId == surveyId);
    }

    public int Delete(int surveyId)
    {
        return context.Surveys.Where(s => s.Id == surveyId).ExecuteDelete();
    }

    public List<SurveyDto> GetAll()
    {
        return context.Surveys.Select(s => new SurveyDto
        {
            Id = s.Id,
            Tilte = s.Name
        }).ToList();
    }

    public ResultSurveyDto ResultSurvey(int surveyId)
    {
        var result = context.Questions
            .Where(q => q.SurveyId == surveyId)
            .Select(q => new QuestionResultDto
            {
                QuestionTitle = q.Title,
                OptionsResults = q.Options.Select(o => new OptionResultDto
                {
                    OptionText = o.Text,
                    OptionCount = o.Votes.Count,
                    OptionPercent = 0 
                }).ToList()
            })
            .ToList();

        var participants = context.Votes
            .Where(v => v.Option.Question.SurveyId == surveyId)
            .Select(v => new
            {
                v.User.FirstName,
                v.User.LastName,
                v.User.UserName
            })
            .Distinct()
            .ToList();

        return new ResultSurveyDto
        {
            Participants = participants.Select(p => new SurveyParticipantsDto
            {
                Name = $"{p.FirstName} {p.LastName}",
                UserName = p.UserName
            }).ToList(),
            QuestionsResult = result
        };
    }


    public bool IsVotedBefore(int surveyId, int userId)
    {
        return context.Votes.Any(v =>
            v.UserId == userId &&
            v.Option.Question.Survey.Id == surveyId);
    }

    public void Save()
    {
        context.SaveChanges();
    }
}