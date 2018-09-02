using System.Net.Http;

namespace HackerNewsScrapper.HttpClients
{
    public interface IHackerNewsClientProvider
    {
        HttpClient GetConfiguredClient();
    }
}
