using AutoMapper;
using MyBlog.Common.Mapping;
using MyBlog.Data.Models;

namespace MyBlog.Services.Models
{
    public class ArticleListingServiceModel : IMapFrom<Article>, IMapExplicitly
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public void RegisterMappings(IProfileExpression profile)
        {
            profile.CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(m => m.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
