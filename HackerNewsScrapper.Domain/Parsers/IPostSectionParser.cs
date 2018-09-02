namespace HackerNewsScrapper.Domain.Parsers
{
    public interface IPostSectionParser
    {
        string GetTitle(string html);
        string GetUrl(string html);
        string GetAuthor(string html);
        int? GetPoints(string html);
        int? GetComments(string html);
        int? GetRank(string html);

    }
}
