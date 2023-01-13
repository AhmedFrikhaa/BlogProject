namespace blog_project.Models
{
    public class RegistrationModel
    {

        
        public string userName { set; get; } 
        public string firstName { set; get; } 
        public string lastName { set; get; } 
        public IFormFile picture { set; get; } 
        public string email { set; get; } 
        public string password { set; get; } 



    }
}
