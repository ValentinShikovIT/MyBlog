using AutoMapper;
using MyBlog.Data.Models;
using MyBlog.Services.Models;

namespace MyBlog.Services.Infrastructure
{
    public class ServiceMappingProfile : Profile
    {
        public ServiceMappingProfile()
        {
            this.CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(m => m.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
            this.CreateMap<Article, ArticleDetailsServiceModel>()
                .ForMember(m => m.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
