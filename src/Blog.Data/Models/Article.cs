using System;
using System.ComponentModel.DataAnnotations;
using static MyBlog.Data.DataValidations.Article;

namespace MyBlog.Data.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
