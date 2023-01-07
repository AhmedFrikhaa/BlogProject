using blog_project.Models;

namespace blog_project.Data
{
    public class BlogRepo
    {
        private Context context;
        public BlogRepo() {}
        public BlogRepo(IConfiguration configuration)
        {
            context = Context.Instantiate_Context(configuration);
        }


        public List<Blog> getUserBlogs(int userId)
        {
            List<Blog> blogs = context.Blog.Where(b => b.userId == userId).ToList() ?? new List<Blog>();
            return blogs;
        }



        public void AddBlog(Blog b)
        {
            context.Blog.Add(b);
            context.SaveChanges();
        }
        public IEnumerable <Blog> GetAllBlogs()
        {
            return context.Blog.ToList();
          
        }
        public void updateBlog(Blog b)
        {
            context.Blog.Update(b);
            context.SaveChanges();
        }
        public void deleteBlog(Blog b)
        {
            context.Blog.Remove(b);
            context.SaveChanges();
        }
        public Blog getBlog(int id)
        {
            return context.Blog.Where(b => b.Id == id).FirstOrDefault();

        }

    }
}
