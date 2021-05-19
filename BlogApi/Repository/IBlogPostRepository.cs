using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.DTOs;

namespace BlogApi.Repository
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> GetAsync(long id);
        Task<IEnumerable<BlogPost>> GetAllAsync();
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(long id);
    }
}
