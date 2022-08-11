﻿using System;

namespace MyBlog.Services.Models
{
    public class ArticleDetailsServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Author { get; set; }
    }
}
