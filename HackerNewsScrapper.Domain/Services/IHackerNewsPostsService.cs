using HackerNewsScrapper.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNewsScrapper.Domain.Services
{
    public interface IHackerNewsPostsService
    {
        Task<List<HackerNewsPost>> GetPosts(int numberOfPosts);

    }
}
