using HackerNewsScrapper.Common;
using HackerNewsScrapper.HttpClients;
using HackerNewsScrapper.ServiceAgents;
using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HackerNewsScrapper.Tests.Unit.Agents
{
    public class HackerNewsServiceAgentTests
    {
        private readonly IHackerNewsServiceAgent _hackerNewsServiceAgent;
        private readonly Mock<IHackerNewsClient> _hackerNewsClient;
        private readonly Mock<IAppConfigurationProvider> _appConfigurationProvider;

        public HackerNewsServiceAgentTests()
        {
            _hackerNewsClient = new Mock<IHackerNewsClient>();
            _appConfigurationProvider = new Mock<IAppConfigurationProvider>();

            _appConfigurationProvider.Setup(c => c.GetIntValue("HttpClientSettings:Retries")).Returns(3);
            _appConfigurationProvider.Setup(c => c.GetIntValue("HttpClientSettings:Delay")).Returns(100);
            _appConfigurationProvider.Setup(c => c.GetStringValue("HttpClientSettings:PageParameter")).Returns("news?p");

            _hackerNewsServiceAgent = new HackerNewsServiceAgent(_hackerNewsClient.Object, _appConfigurationProvider.Object);
        }

        [Fact]
        public async Task Should()
        {
            int page = 1;
            string result = "<html></html>";
            string expectedRequestString = $"news?p={page}";

            var respone = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(result)
            };

            _hackerNewsClient.Setup(c => c.GetRawPageData(It.Is<string>(p=> p.Equals(expectedRequestString))))
                .ReturnsAsync(respone);
            
            var html = await _hackerNewsServiceAgent.GetDataFromPage(page).ConfigureAwait(false); ;

            Assert.Equal(result, html);
        }



        [Fact]
        public async Task ShouldRetryUntilFail()
        {
            int page = 1;
            string expectedRequestString = $"news?p={page}";

            var respone = new HttpResponseMessage(HttpStatusCode.BadGateway);

            _hackerNewsClient.Setup(c => c.GetRawPageData(It.IsAny<string>()))
                .ReturnsAsync(respone);

            await _hackerNewsServiceAgent.GetDataFromPage(page).ConfigureAwait(false); ;

            _hackerNewsClient.Verify(c => c.GetRawPageData(It.IsAny<string>()), Times.AtLeast(3));
        }
    }
}
