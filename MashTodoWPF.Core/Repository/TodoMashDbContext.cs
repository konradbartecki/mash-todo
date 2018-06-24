using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MashTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace MashTodo.Repository
{
    public class TodoMashDbContext : DbContext
    {
        public TodoMashDbContext (DbContextOptions<TodoMashDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoItem> TodoItem { get; set; }
    }
}
