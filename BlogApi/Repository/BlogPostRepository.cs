using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogPostContext context;

        public BlogPostRepository(BlogPostContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GetAsync(long id)
        {
            return await context.BlogPosts.FindAsync(id);
        }

        public async Task AddAsync(BlogPost post)
        {
            var isTitleAlreadyExisting = await context.BlogPosts.AnyAsync(x => x.Title.Equals(post.Title));

            if (isTitleAlreadyExisting)
            {
                throw new BlogPostsDomainException($"Blog post with such title already exist: {post.Title}");
            }

            await context.BlogPosts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost post)
        {
            var isSuchTitleAlreadyExisting = await context.BlogPosts.AnyAsync(x => x.Title.Equals(post.Title) && x.Id != post.Id);

            if (isSuchTitleAlreadyExisting)
            {
                throw new BlogPostsDomainException($"Blog post with such title already exist: {post.Title}");
            }

            var existingPost = await context.BlogPosts.FindAsync(post.Id);
            context.Entry(existingPost).CurrentValues.SetValues(post);

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(long id)
        {
            var post = await context.BlogPosts.FindAsync(id);
            if (post != null)
            {
                context.BlogPosts.Remove(post);
                await context.SaveChangesAsync();
            }
        }
    }
}
