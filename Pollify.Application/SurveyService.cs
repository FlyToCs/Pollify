using Pollify.Application.Contracts;
using Pollify.Domain.DTOs;
using Pollify.Domain.Entities.SurveyAgg;

namespace Pollify.Application;

public class SurveyService(ISurveyRepository surveyRepository) : ISurveyService
{
    public int Create(string title,int userId)
    {
        return surveyRepository.Create(title,userId);
    }

    public int Delete(int surveyId)
    {
        if (!surveyRepository.SurveyHasParticipant(surveyId))
            throw new Exception("you can't delete this survey, this survey doesn't have any participants");
        
        return surveyRepository.Delete(surveyId);
    }

    public List<SurveyDto> GetAll()
    {
        return surveyRepository.GetAll();
    }

    public ResultSurveyDto ResultSurvey(int surveyId)
    {
        var resultDto = surveyRepository.ResultSurvey(surveyId);

        foreach (var question in resultDto.QuestionsResult)
        {
            int totalVotes = question.OptionsResults.Sum(o => o.OptionCount);

            foreach (var option in question.OptionsResults)
            {
                option.OptionPercent = totalVotes == 0
                    ? 0
                    : Math.Round(((decimal)option.OptionCount / totalVotes) * 100, 2);
            }
        }

        resultDto.TotalParticipantCount = resultDto.Participants.Count;
        return resultDto;
    }


    public bool IsVotedBefore(int surveyId, int userId)
    {
        return surveyRepository.IsVotedBefore(surveyId,userId);
    }
}