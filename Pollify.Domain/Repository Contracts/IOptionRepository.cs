namespace Pollify.Domain.Repository_Contracts;

public interface IOptionRepository
{
    int Create(string text, int questionId);
}