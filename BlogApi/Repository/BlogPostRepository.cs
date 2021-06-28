using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository
{
    public class BlogPostRepository
    {
        public readonly BlogPostContext context;
        public bool isTitleAlreadyExisting;
        public bool isSuchTitleAlreadyExisting;

        public BlogPostRepository(BlogPostContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<BlogPost>> GettingAllBooksFromRepository()
        {
            return await context.BlogPosts.ToListAsync();
        }

        public async Task<BlogPost> GettingOnlyOneBook(long id)
        {
            return await context.BlogPosts.FindAsync(id);
        }

        public async Task AddingABookToRepository(BlogPost post)
        {
            CheckIfIsTitleAlreadyExisting(post);

            await context.BlogPosts.AddAsync(post);
            await context.SaveChangesAsync();
        }

        public async void CheckIfIsTitleAlreadyExisting(BlogPost post)
        {
            isTitleAlreadyExisting = await context.BlogPosts.AnyAsync(x => x.Title.Equals(post.Title));

            if (isTitleAlreadyExisting)
            {
                throw new BlogPostsDomainException($"Blog post with such title already exist: {post.Title}");
            }
        }

        public async Task UpdatingABookInRepository(BlogPost post)
        {
            CheckIfIsSuchTitleAlreadyExisting(post);

            var existingPost = await context.BlogPosts.FindAsync(post.Id);
            context.Entry(existingPost).CurrentValues.SetValues(post);

            await context.SaveChangesAsync();
        }

        public async void CheckIfIsSuchTitleAlreadyExisting(BlogPost post)
        {
            isSuchTitleAlreadyExisting = await context.BlogPosts.AnyAsync(x => x.Title.Equals(post.Title) && x.Id != post.Id);

            if (isSuchTitleAlreadyExisting)
            {
                throw new BlogPostsDomainException($"Blog post with such title already exist: {post.Title}");
            }
        }

        public async Task RemovalOfBookFromRepository(long id)
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
