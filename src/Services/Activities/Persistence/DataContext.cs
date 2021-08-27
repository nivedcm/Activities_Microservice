using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }
        public DataContext()
        {

        }

        public DbSet<Activity> Activities { get; set; }
    }
}