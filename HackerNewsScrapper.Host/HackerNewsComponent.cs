using AutoMapper;
using HackerNewsScrapper.Domain.Services;
using HackerNewsScrapper.Host.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNewsScrapper.Host
{
    public class HackerNewsComponent
    {
        private readonly IHackerNewsPostsService _postService;
        private readonly IMapper _mapper;
        private readonly JsonSerializerSettings _settings;

        public HackerNewsComponent(IHackerNewsPostsService hackerNewsPostsService, IMapper mapper)
        {
            _postService = hackerNewsPostsService;
            _mapper = mapper;

            _settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            };
        }

        public async Task<string> GetPosts(int numberOfPosts)
        {
            var hackerNewsPosts = await _postService.GetPosts(numberOfPosts).ConfigureAwait(false);
            List<Post> posts = hackerNewsPosts.Select(p => _mapper.Map<Post>(p)).ToList();

            return GetJson(posts);

        }

        private string GetJson(object obj)
        {
            var json = JsonConvert.SerializeObject(obj, _settings);
            return json;
        }
    }
}
