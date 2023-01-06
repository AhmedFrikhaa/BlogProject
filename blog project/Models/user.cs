using System.ComponentModel.DataAnnotations;

namespace blog_project.Models
{
    public class User
    {
        public User(string userName, string firstName, string lastName, string picture, string email, string password)
        {
            this.userName = userName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.picture = picture;
            this.email = email;
            this.password = password;
        }

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
