using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsScrapper.HttpClients
{
    public class HackerNewsClient: IHackerNewsClient
    {
        private readonly IHackerNewsClientProvider _hackerNewsClientProvider;

        public HackerNewsClient(IHackerNewsClientProvider hackerNewsClientProvider)
        {
            _hackerNewsClientProvider = hackerNewsClientProvider;
        }

        public async Task<HttpResponseMessage> GetRawPageData(string request)
        {
            var client = _hackerNewsClientProvider.GetConfiguredClient();
            return await client.GetAsync(request).ConfigureAwait(false);
        }
    }
}
