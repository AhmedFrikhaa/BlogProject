﻿namespace blog_project.Models
{
    public class CreateBlog
    {
        public string title { set; get; }
        public string description { set; get; } 
        public IFormFile image { set; get; }
        public string theme { set; get; }

    }
}
