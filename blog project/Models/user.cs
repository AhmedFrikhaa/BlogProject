using System.ComponentModel.DataAnnotations;

namespace blog_project.Models
{
    public class user
    {
        [Key]
        public string username { set; get; } = string.Empty;
        public string firstName { set; get; } = string.Empty;
        public string lastName { set; get; } = string.Empty;
        public string? picture { set; get; } = string.Empty;
        public string email { set; get; } = string.Empty;
        public string password { set; get; } = string.Empty;
        
        


    }
}
