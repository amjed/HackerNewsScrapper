using Supremes;
using System.Text.RegularExpressions;

namespace HackerNewsScrapper.Domain.Parsers
{
    public class PostSectionParser : IPostSectionParser
    {
        public string GetAuthor(string html)
        {
            var element = Dcsoup.Parse(html);
            return element.Select("a.hnuser").Text;
        }

        public int? GetComments(string html)
        {
            var element = Dcsoup.Parse(html);
            var text = element.Select("a").Last.Text;
            var matchValue = Regex.Match(text, "[0-9]+").Value;
            if (string.IsNullOrEmpty(matchValue))
                return null;
            return int.Parse(matchValue);
        }

        public int? GetPoints(string html)
        {
            var element = Dcsoup.Parse(html);
            var text = element.Select("td.subtext > span.score").Text;
            var matchValue = Regex.Match(text, "[0-9]+").Value;
            if (string.IsNullOrEmpty(matchValue))
                return null;
            return int.Parse(matchValue);
        }

        public int? GetRank(string html)
        {
            var element = Dcsoup.Parse(html);
            var text = element.Select("span.rank").Text;
            var matchValue = Regex.Match(text, "[0-9]+").Value;
            if (string.IsNullOrEmpty(matchValue))
                return null;
            return int.Parse(matchValue);
        }

        public string GetTitle(string html)
        {
            var element = Dcsoup.Parse(html);
            return element.Select("td.title > a.storylink").Text;
        }

        public string GetUrl(string html)
        {
            var element = Dcsoup.Parse(html);
            return element.Select("td.title > a.storylink").Attr("href");
        }
    }
}
