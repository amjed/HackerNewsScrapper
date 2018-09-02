//using HackerNewsScrapper.HttpClients;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;
//using Xunit;

//namespace HackerNewsScrapper.Tests.Unit.Clients
//{
//    public class HackerNewsClientTests
//    {
//        private readonly IHackerNewsClient _hackerNewsClient;
//        private readonly Mock<IHackerNewsClientProvider> _hackerNewsClientProvider;

//        public HackerNewsClientTests()
//        {
//            _hackerNewsClientProvider = new Mock<IHackerNewsClientProvider>();
//            _hackerNewsClientProvider.Setup(c => c.GetConfiguredClient()).Returns(Mock<HttpClient>);
//            _hackerNewsClient = new HackerNewsClient(_hackerNewsClientProvider.Object);
//        }

//        [Fact]
//        public async Task ShouldCallHttpClientGetAsync()
//        {
//            var result = await _hackerNewsClient.GetRawPageData("/p=1");
//            Assert.NotEmpty(result);

//        }
//    }
//}
