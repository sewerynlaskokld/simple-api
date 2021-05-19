using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BlogApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogPostsController : Controller
    {
        private readonly ILogger<BlogPostsController> _logger;
        private readonly BlogPostRepository postsRepo;

        public BlogPostsController(ILogger<BlogPostsController> logger, BlogPostContext kontekst_bazy)
        {
            _logger = logger;
            postsRepo = new BlogPostRepository(kontekst_bazy);
        }

        [HttpGet("getBooks")] // GET api/blogposts/getBooks
        [ProducesResponseType(200, Type = typeof(IEnumerable<BlogPost>))]
        public async Task<ActionResult<IEnumerable<BlogPost>>> GetAll()
        {
            _logger.LogInformation("Obtaining all the blog posts");
            var posts = postsRepo.GettingAllBooksFromRepository().Result;
            _logger.LogDebug("Retrieved {0} posts total", posts.ToList().Count);

            return Ok(posts);
        }

        [HttpGet("getBook/{id}", Name = "GetBlogPost")] // GET api/blogposts/getBook/5
        [ProducesResponseType(200, Type = typeof(BlogPost))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BlogPost>> Get(long id)
        {
            _logger.LogInformation("Obtaining post {Id}", id);

            var item = postsRepo.GettingOnlyOneBook(id).Result;
            if (item == null)
            {
                _logger.LogWarning("Post {Id} not found", id);
                return BadRequest();
            }

            return Ok(item);
        }

        [HttpPut("addBook/{id}")] // POST api/addBook/1
        [ProducesResponseType(201, Type = typeof(BlogPost))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Create([FromBody] BlogPost post)
        {
            _logger.LogInformation("Adding new blog post");

            await postsRepo.AddingABookToRepository(post);

            _logger.LogInformation("Post {0} has been added", post.Id);
            return CreatedAtRoute("GetBlogPost", new { id = post.Id }, post);
        }

        [HttpPut("updateBook/{id}")] // PUT api/updateBook/1
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Update(long id, [FromBody] BlogPost updatedPost)
        {
            _logger.LogInformation("Updating post {0}", id);
            _logger.LogDebug("Received post id {0} with new title: {1}'", id, updatedPost.Title);

            var post = postsRepo.GettingOnlyOneBook(id).Result;
            if (post == null)
            {
                _logger.LogWarning("Post {0} not found", id);
                return BadRequest();
            }

            updatedPost.Id = id;
            await postsRepo.UpdatingABookInRepository(updatedPost);

            _logger.LogInformation("Updating post {0} succeeded", post.Id);
            return Ok();
        }

        [HttpDelete("deleteBook/{id}")] // DELETE api/blogposts/deleteBook/5
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete(long id)
        {
            _logger.LogInformation("Removing post {id}", id);

            var post = postsRepo.GettingOnlyOneBook(id).Result;
            if (post == null)
            {
                _logger.LogWarning("Post {id} not found", id);
                return BadRequest();
            }

            await postsRepo.RemovalOfBookFromRepository(id);

            _logger.LogInformation("Removing post {id} succeeded", id);
            return Ok();
        }
    }
}
