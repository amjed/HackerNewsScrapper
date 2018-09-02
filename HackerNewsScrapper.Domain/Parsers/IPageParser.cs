using HackerNewsScrapper.Entities;
using System.Collections.Generic;

namespace HackerNewsScrapper.Domain.Parsers
{
    public interface IPageParser
    {
        bool IsPageValid(string html);
        List<HackerNewsPost> GetHackerNewsPosts(string pageHtml);
    }
}
