using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNewsScrapper.Domain.Parsers;
using HackerNewsScrapper.Domain.Validation;
using HackerNewsScrapper.Entities;
using HackerNewsScrapper.ServiceAgents;

namespace HackerNewsScrapper.Domain.Services
{
    public class HackerNewsPostsService : IHackerNewsPostsService
    {
        private readonly IHackerNewsServiceAgent _hackerNewsServiceAgent;
        private readonly IPageParser _pageParser;
        private readonly IHackerNewsPostItemValidator _hackerNewsPostItemValidator;

        public HackerNewsPostsService(IHackerNewsServiceAgent hackerNewsServiceAgent, IPageParser pageParser, IHackerNewsPostItemValidator hackerNewsPostItemValidator)
        {
            _hackerNewsServiceAgent = hackerNewsServiceAgent;
            _pageParser = pageParser;
            _hackerNewsPostItemValidator = hackerNewsPostItemValidator;
        }

        public async Task<List<HackerNewsPost>> GetPosts(int numberOfPosts)
        {
            List<HackerNewsPost> result = new List<HackerNewsPost>();
            int currentPage = 1;

            do
            {
                var pageData = await _hackerNewsServiceAgent.GetDataFromPage(currentPage++).ConfigureAwait(false);
                if (_pageParser.IsPageValid(pageData))
                {
                    var posts = _pageParser.GetHackerNewsPosts(pageData);
                    posts.ForEach(p =>
                    {
                        if (_hackerNewsPostItemValidator.IsValid(p))
                            result.Add(p);
                    });
                }
            } while (result.Count < numberOfPosts);

            if (result.Count > numberOfPosts)
            {
                result = result.Take(numberOfPosts).ToList();
            }
            return result;
        }
    }
}
