using blog_project.Models;

namespace blog_project.Data
{
    public class blogRepo
    {
        private  blogContext context;
        public blogRepo() {}
        public blogRepo(IConfiguration configuration)
        {
            context = blogContext.Instantiate_blogContext(configuration);
        }
        public void AddBlog(blog b)
        {
            context.Blog.Add(b);
            context.SaveChanges();
        }
        public IEnumerable <blog> GetAllBlogs()
        {
            return context.Blog.ToList();
          
        }
        public void updateBlog(blog b)
        {
            context.Blog.Update(b);
            context.SaveChanges();
        }
        public void deleteBlog(blog b)
        {
            context.Blog.Remove(b);
            context.SaveChanges();
        }
        public blog getBlog(int id)
        {
            return context.Blog.Where(b => b.Id == id).FirstOrDefault();

        }

    }
}
