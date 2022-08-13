using AutoMapper;
using MyBlog.Common.Mapping;
using MyBlog.Data.Models;
using System;

namespace MyBlog.Services.Models
{
    public class ArticleDetailsServiceModel : IMapFrom<Article>, IMapExplicitly
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }

        public void RegisterMappings(IProfileExpression profile)
        {
            profile.CreateMap<Article, ArticleDetailsServiceModel>()
                .ForMember(m => m.Author, cfg => cfg.MapFrom(a => a.Author.UserName));
        }
    }
}
