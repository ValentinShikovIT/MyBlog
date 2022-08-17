using AutoMapper;
using MyBlog.Common.Mapping;
using MyBlog.Services.Models;

namespace MyBlog.Controllers.Models.Articles
{
    public class ArticleDisplayModel : IMapFrom<ArticleListingServiceModel>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }
    }
}
