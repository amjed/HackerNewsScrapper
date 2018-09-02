using AutoMapper;
using HackerNewsScrapper.Entities;
using HackerNewsScrapper.Host.ViewModel;

namespace HackerNewsScrapper.Host.AutoMapperProfiles
{
    public class PostProfile : Profile    {
        public PostProfile()
        {
            CreateMap<HackerNewsPost, Post>()
                .ForMember(d => d.Uri, opt => opt.MapFrom(m => m.Url));
        }
    }
}
