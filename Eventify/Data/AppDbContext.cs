using Eventify.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventify.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=eventify.db");
        }
    }
}
