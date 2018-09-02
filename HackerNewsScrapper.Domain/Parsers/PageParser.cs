using HackerNewsScrapper.Entities;
using Supremes;
using System.Collections.Generic;
using System.Text;

namespace HackerNewsScrapper.Domain.Parsers
{
    public class PageParser: IPageParser
    {
        private readonly IPostSectionParser _postSectionParser;

        public PageParser(IPostSectionParser postSectionParser)
        {
            _postSectionParser = postSectionParser;
        }

        public bool IsPageValid(string html)
        {
            return string.IsNullOrEmpty(html) == false && html.Contains("athing");
        }

        private List<string> GetPostsSectionsFromPage(string html)
        {
            var document = Dcsoup.Parse(html);
            var postSectionSelector = "athing";
            var postSections = document.GetElementsByClass(postSectionSelector);

            var sectionsHtml = new List<string>();
            foreach (var section in postSections)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("<table>")
                    //because interesting data is in two rows
                    .Append(section.OuterHtml)
                    .Append(section.NextSibling.OuterHtml)
                .Append("</table>");

                sectionsHtml.Add(sb.ToString());
            }

            return sectionsHtml;
        }

        public List<HackerNewsPost> GetHackerNewsPosts(string pageHtml)
        {
            var result = new List<HackerNewsPost>();
            var sections = GetPostsSectionsFromPage(pageHtml);
            foreach (var sectionHtml in sections)
            {
                var post = new HackerNewsPost
                {
                    Author = _postSectionParser.GetAuthor(sectionHtml),
                    Title = _postSectionParser.GetTitle(sectionHtml),
                    Url = _postSectionParser.GetUrl(sectionHtml),
                    Comments = _postSectionParser.GetComments(sectionHtml),
                    Points = _postSectionParser.GetPoints(sectionHtml),
                    Rank = _postSectionParser.GetRank(sectionHtml),

                };

                result.Add(post);
            }

            return result;
        }
    }
}
