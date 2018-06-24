using MashTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace MashTodo.Repository
{
    public class TodoMashDbContext : DbContext
    {
        public TodoMashDbContext(DbContextOptions<TodoMashDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public DbSet<TodoItem> TodoItem { get; set; }
    }
}