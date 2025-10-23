using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.SurveyAgg;
using Pollify.Infrastructure.EfCore.persistence;

namespace Pollify.Infrastructure.EfCore.Repositories;

public class SurveyRepository(AppDbContext context) : ISurveyRepository
{
    public void Create(CreateSurveyDto createSurveyDto)
    {
        var survey = new Survey
        {
            Name = createSurveyDto.SurveyTitle,
            UserId = createSurveyDto.UserId,
            Questions = []
        };

        foreach (var questionDto in createSurveyDto.QuestionDtos)
        {
            var question = new Question
            {
                Title = questionDto.Text, 
                Survey = survey, 
                Options = []
            };

            foreach (var optionText in questionDto.Options)
            {
                var option = new Option(optionText)
                {
                    Question = question 
                };

                question.Options.Add(option);
            }

            survey.Questions.Add(question);
        }

        context.Surveys.Add(survey);
    }

    public bool IsSurveyTitleExist(string title)
    {
        return context.Surveys.Any(s => s.Name == title);
    }

    public bool SurveyHasParticipant(int surveyId)
    {
        throw new NotImplementedException();
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
        var participants = context.Votes
            .Where(v => v.Option.Question.SurveyId == surveyId)
            .Select(v => v.User)
            .Distinct()
            .Select(u => new SurveyParticipantsDto
            {
                Name = $"{u.FirstName} {u.LastName}",
                UserName = u.UserName
            })
            .ToList(); 

        var questions = context.Questions
            .Where(q => q.SurveyId == surveyId)
            .Include(q => q.Options)
            .ThenInclude(o => o.Votes)
            .ToList();

        var questionResults = new List<QuestionResultDto>();

        foreach (var q in questions)
        {
            int totalVotesForThisQuestion = q.Options.Sum(o => o.Votes.Count);

            var optionResults = new List<OptionResultDto>();
            foreach (var o in q.Options)
            {
                int voteCount = o.Votes.Count;
                decimal percentage = (totalVotesForThisQuestion == 0)
                                     ? 0
                                     : ((decimal)voteCount / totalVotesForThisQuestion) * 100;

                optionResults.Add(new OptionResultDto
                {
                    OptionText = o.Text, 
                    OptionCount = voteCount,
                    OptionPercent = Math.Round(percentage, 2) 
                });
            }

            questionResults.Add(new QuestionResultDto
            {
                QuestionTitle = q.Title,
                OptionsResults = optionResults
            });
        }

    
        var result = new ResultSurveyDto
        {
            TotalParticipantCount = participants.Count,
            Participants = participants,
            QuestionsResult = questionResults 
        };

        return result;
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