using blog_project.Models;

namespace blog_project.Data
{
    public class Context : DbContext
    {
        private static Context context=null;

        
        public Context(DbContextOptions o) : base(o)
        {

        }

        public static Context Instantiate_Context(IConfiguration configuration)
        {
            if (context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<Context>();
                optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("sqlite")}");
                context = new Context(optionsBuilder.Options);
                return context;
            }
            return context;
        }

        public DbSet<User> User  { get; set; }

        public DbSet<Blog> Blog { get; set; }
    }
}
