using Microsoft.EntityFrameworkCore;
using ToDoAPI.Model;

namespace ToDoAPI.Data
{
    public class ToDoDbContext:DbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options)
        {

        }
        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
