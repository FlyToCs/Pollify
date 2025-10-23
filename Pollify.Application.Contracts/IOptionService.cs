namespace Pollify.Application.Contracts;

public interface IOptionService
{
    int Create(string text, int questionId);
}