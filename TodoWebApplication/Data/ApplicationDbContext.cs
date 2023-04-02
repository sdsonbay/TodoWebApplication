using Microsoft.EntityFrameworkCore;
using TodoWebApplication.Models;

namespace TodoWebApplication.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
    public DbSet<Subtodo> Subtodos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(e => e.Todos)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .HasPrincipalKey(e => e.Id);

        modelBuilder.Entity<Todo>()
            .HasMany(e => e.Subtodos)
            .WithOne(e => e.Todo)
            .HasForeignKey(e => e.TodoId)
            .HasPrincipalKey(e => e.Id);
    }
}