using System.ComponentModel.DataAnnotations;

namespace blog_project.Models
{
    public class Blog
    {
        
        [Key]
        public int Id { set; get; }
        public string title { set; get; } 
        public string? description { set; get; } 
        public string? image { set; get; } 
        public DateTime date { set; get; }
        public User user { set; get; }
        public int userId { set; get; }
        
    }
}
