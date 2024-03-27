using Microsoft.EntityFrameworkCore;

namespace WebApp.Models
{
    public class WebAppDBContext : DbContext
    {
        public WebAppDBContext() {}
        public WebAppDBContext(DbContextOptions<WebAppDBContext> options) 
            : base(options) { }

        public DbSet<Car> Cars { get; set; }
    }
}
