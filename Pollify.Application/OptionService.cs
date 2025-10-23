using Pollify.Application.Contracts;
using Pollify.Domain.Repository_Contracts;

namespace Pollify.Application;

public class OptionService(IOptionRepository optionRepository) : IOptionService
{
    public int Create(string text, int questionId)
    {
        return optionRepository.Create(text,questionId);
    }
}