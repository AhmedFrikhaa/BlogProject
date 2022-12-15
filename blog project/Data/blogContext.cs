using blog_project.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_project.Data
{
    public class blogContext :DbContext
    {
        private static blogContext context=null;
        private blogContext(DbContextOptions o) : base(o)
        {

        }

        public static blogContext Instantiate_blogContext(IConfiguration configuration)
        {
            if (context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<blogContext>();
                optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("sqlite")}");
                context = new blogContext(optionsBuilder.Options);
                return context;
            }
            return context;


        }
        public DbSet<blog> Blog { get; set; }
    }
}
