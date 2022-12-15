using blog_project.Models;
using Microsoft.EntityFrameworkCore;

namespace blog_project.Data
{
    public class userContext : DbContext
    {
        private static userContext context;
        private  userContext(DbContextOptions o) : base(o)
        {

        }

        public static userContext Instantiate_userContext(IConfiguration configuration)
        {
            if (context == null)
            {
                var optionsBuilder = new DbContextOptionsBuilder<userContext>();
                optionsBuilder.UseSqlite($"Filename={configuration.GetConnectionString("sqlite")}");
                context = new userContext(optionsBuilder.Options);
                return context;
            }
            return context;
        }
        public DbSet<user> User { get; set; }
    }
}
