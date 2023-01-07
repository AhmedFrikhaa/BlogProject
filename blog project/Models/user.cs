using System.ComponentModel.DataAnnotations;

namespace blog_project.Models
{
    public class User
    {
        

        [Key]
        public int id { get; set; }
        public string userName  { set; get; } 
        public string firstName { set; get; } 
        public string lastName { set; get; } 
        public string picture { set; get; } 
        public string email { set; get; } 
        public string password { set; get; } 
        public List<Blog> blogs { set; get; } = new List<Blog>();   
        
        


    }
}
