using System.Threading.Tasks;

namespace HackerNewsScrapper.ServiceAgents
{
    public interface IHackerNewsServiceAgent
    {
        Task<string> GetDataFromPage(int pageNumber);
    }
}
