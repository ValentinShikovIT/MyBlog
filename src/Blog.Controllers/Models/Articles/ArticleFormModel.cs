using System.ComponentModel.DataAnnotations;
using static MyBlog.Data.DataValidations.Article;

namespace MyBlog.Controllers.Models.Articles
{
    public class ArticleFormModel
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }
    }
}
