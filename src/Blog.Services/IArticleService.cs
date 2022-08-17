using MyBlog.Services.Common;
using MyBlog.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBlog.Services
{
    public interface IArticleService : IService
    {
        public Task<int> CreateAsync(string title, string description, string authorId);

        public Task<ArticleDetailsServiceModel> Details(int id);

        public Task<IEnumerable<ArticleListingServiceModel>> All(int page);

        public Task<bool> Edit(int id, string title, string description);

        public Task<bool> Exists(int Id, string authorId);

        public Task<bool> Remove(int id);
    }
}
