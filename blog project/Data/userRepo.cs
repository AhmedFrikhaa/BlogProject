using blog_project.Models;

namespace blog_project.Data
{
    public class userRepo
    {
        private userContext context;
        public userRepo() { }
        public userRepo(IConfiguration configuration)
        {
            context = userContext.Instantiate_userContext(configuration);
        }
        public void AddUser(user u)
        {
            context.User.Add(u);
            context.SaveChanges();
        }
        public IEnumerable<user> GetAllUsers()
        {
            return context.User.ToList();
        }
        public user GetUser(string username)
        {
            return context.User.Where(u => u.username == username).FirstOrDefault();
        }
        public void UpdateUser(user u)
        {
            context.User.Update(u);
            context.SaveChanges();
        }
        public void DeleteUser(user u)
        {
            context.User.Remove(u);
            context.SaveChanges();
        }
        


    }
}
