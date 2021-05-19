using BlogApi.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Data
{
    public class BlogPostContext : DbContext
    {
        public BlogPostContext(DbContextOptions<BlogPostContext> options)
            : base(options)
        {
            // nop
        }

        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BlogPost>().HasData
            (
                new BlogPost { Id = 1, Title = "Test 1", Description = "Descrition 1" },
                new BlogPost { Id = 2, Title = "Test 2", Description = "Descrition 2" },
                new BlogPost { Id = 3, Title = "Test 3", Description = "Descrition 3" },
                new BlogPost { Id = 4, Title = "Test 4", Description = "Descrition 4" },
                new BlogPost { Id = 5, Title = "Test 5", Description = "Descrition 5" }
            );

        }
    }
}
