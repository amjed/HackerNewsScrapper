using HackerNewsScrapper.Domain.Parsers;
using HackerNewsScrapper.Domain.Services;
using HackerNewsScrapper.Domain.Validation;
using HackerNewsScrapper.Entities;
using HackerNewsScrapper.ServiceAgents;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsScrapper.Tests.Unit.Domain
{
    public class HackerNewsPostsServiceTests
    {
        private readonly IHackerNewsPostsService _hackerNewsPostsService;
        private readonly Mock<IHackerNewsServiceAgent> _hackerNewsServiceAgent;
        private readonly Mock<IPageParser> _pageParser;
        private readonly Mock<IHackerNewsPostItemValidator> _hackerNewsPostItemValidator;

        public HackerNewsPostsServiceTests()
        {
            _hackerNewsServiceAgent = new Mock<IHackerNewsServiceAgent>();
            _pageParser = new Mock<IPageParser>();
            _hackerNewsPostItemValidator = new Mock<IHackerNewsPostItemValidator>();

            _pageParser.Setup(c => c.IsPageValid(It.IsAny<string>())).Returns(true);
            _hackerNewsPostItemValidator.Setup(c => c.IsValid(It.IsAny<HackerNewsPost>())).Returns(true);

            _hackerNewsPostsService = new HackerNewsPostsService(_hackerNewsServiceAgent.Object,
                _pageParser.Object, _hackerNewsPostItemValidator.Object);
        }

        [Fact]
        public async Task ShouldRetrieveDesiredNumberOfPosts()
        {
            var desiredPosts = 30;
            _hackerNewsServiceAgent.Setup(c => c.GetDataFromPage(It.IsAny<int>())).ReturnsAsync("<html></html>");
            _pageParser.Setup(c => c.GetHackerNewsPosts(It.IsAny<string>())).Returns(GenerateData(desiredPosts));
            var posts = await _hackerNewsPostsService.GetPosts(desiredPosts).ConfigureAwait(false); ;
            Assert.Equal(desiredPosts, posts.Count);
        }

        [Fact]
        public async Task ShouldMakeNumberOfCalls()
        {
            var desiredPosts = 30;
            var postsPerPage = 10;
            var expectedCalls = desiredPosts / postsPerPage;

            _hackerNewsServiceAgent.Setup(c => c.GetDataFromPage(It.IsAny<int>())).ReturnsAsync("<html></html>");
            _pageParser.Setup(c => c.GetHackerNewsPosts(It.IsAny<string>())).Returns(GenerateData(postsPerPage));
            var posts = await _hackerNewsPostsService.GetPosts(desiredPosts).ConfigureAwait(false); ;

            Assert.Equal(desiredPosts, posts.Count);
            _pageParser.Verify(c => c.GetHackerNewsPosts(It.IsAny<string>()), Times.Exactly(expectedCalls));

        }


        private List<HackerNewsPost> GenerateData(int count)
        {
            var resultList = new List<HackerNewsPost>();
            for(int i=0;i<count;i++)
            {
                resultList.Add(new HackerNewsPost
                {
                    Author = "Someone",
                    Comments = 1,
                    Points = 1,
                    Rank = 1,
                    Title = "Some Title",
                    Url = "https://someurl.tld"
                });
            }
            return resultList;
        }
    }
}
