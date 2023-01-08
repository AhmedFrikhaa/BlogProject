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
        public void AddBlog(Blog b)
        {
            context.Blog.Add(b);
            context.SaveChanges();
        }
        public IEnumerable <Blog> GetAllBlogs()
        {

            List<Blog> blogs =context.Blog.ToList();
            return blogs;

          
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
