using Microsoft.EntityFrameworkCore;
using TodoList.Services.Database.Entities;

namespace TodoList.Services.Database.Context;

public class TodoListDbContext : DbContext
{
    public TodoListDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<TodoListEntity> TodoLists { get; set; }

    public DbSet<TaskEntity> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
