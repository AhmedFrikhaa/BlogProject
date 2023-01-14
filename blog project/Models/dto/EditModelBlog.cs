using System.ComponentModel.DataAnnotations;


namespace blog_project.Models.dto
{
    public class EditModelBlog
    {
        public int Id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public string? image { set; get; }
        public string? theme { set; get; }
    }
}