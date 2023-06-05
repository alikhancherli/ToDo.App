using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDo.App.Domain.Entities;

namespace ToDo.App.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);

        }

        public DbSet<ToDoList> TodoLists => Set<ToDoList>();

        public DbSet<ToDoItem> TodoItems => Set<ToDoItem>();
    }
}
