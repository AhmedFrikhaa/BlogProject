using System.ComponentModel.DataAnnotations;

namespace blog_project.Models
{
    public class blog
    {
        [Key]
        public int Id { set; get; }
        public string title { set; get; } = string.Empty;
        public string? description { set; get; } = string.Empty;
        public string? image { set; get; } = string.Empty;
        public DateTime date { set; get; }
        public user user { set; get; }
        
    }
}
