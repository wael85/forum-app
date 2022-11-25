using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class ForumContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/forum.db");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>().HasKey(post => post.Id);
        modelBuilder.Entity<Post>().Property(post => post.Title).HasMaxLength(50);

        modelBuilder.Entity<User>().HasKey(user => user.Id);
       modelBuilder.Entity<User>().HasMany<Post>();

    }
}