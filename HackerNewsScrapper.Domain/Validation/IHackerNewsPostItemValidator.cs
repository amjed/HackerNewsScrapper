using HackerNewsScrapper.Entities;

namespace HackerNewsScrapper.Domain.Validation
{
    public interface IHackerNewsPostItemValidator
    {
        bool IsValid(HackerNewsPost post);
    }
}
