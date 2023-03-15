using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace ToDoAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Todo> ToDos { get; set; }

    }
}
