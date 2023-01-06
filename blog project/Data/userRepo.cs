using blog_project.Models;

namespace blog_project.Data
{
    public class UserRepo
    {
        private Context context;
        public UserRepo(IConfiguration configuration)
        {
            context = Context.Instantiate_Context(configuration);
        }
        public void AddUser(User u)
        {
            context.User.Add(u);
            context.SaveChanges();
        }
        public IEnumerable<User> GetAllUsers()
        {
            return context.User.ToList();
        }
        public User GetUser(string username)
        {
            return context.User.Where(u => u.userName == username).FirstOrDefault();
        }

        public User? getUserByEmail(string email)
        {
            return context.User.Where(u =>u.email == email).FirstOrDefault();
        }

        public void UpdateUser(User u)
        {
            context.User.Update(u);
            context.SaveChanges();
        }
        public void DeleteUser(User u)
        {
            context.User.Remove(u);
            context.SaveChanges();
        }
        


    }
}
