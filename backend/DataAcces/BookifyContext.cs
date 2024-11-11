using Interfaces.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAcces
{
    public class BookifyContext : DbContext
    {
        public BookifyContext(DbContextOptions<BookifyContext> options)
            : base(options)
        {
        }
        public DbSet<UserDto> Users { get; set; }
        public DbSet<PostDto> Posts { get; set; }
        public DbSet<CommentDto> Comments { get; set; }
        public DbSet<LikeDto> Likes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDto>().ToTable("User");
            modelBuilder.Entity<PostDto>().ToTable("Post");
            modelBuilder.Entity<CommentDto>().ToTable("Comment");
            modelBuilder.Entity<LikeDto>().ToTable("Like");
        }
    }
}
