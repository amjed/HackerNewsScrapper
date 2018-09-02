using System.Net.Http;
using System.Threading.Tasks;

namespace HackerNewsScrapper.HttpClients
{
    public interface IHackerNewsClient
    {
        Task<HttpResponseMessage> GetRawPageData(string request);
    }
}
