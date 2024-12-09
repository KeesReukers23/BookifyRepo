using bookifyWEBApi.ExportModels;
using bookifyWEBApi.ImportModels;
using Interfaces;
using Logic.Entities;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookifyWEBApi.Controllers
{
    [Authorize] //Jwt token check
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        // GET: api/posts/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<PostEx>>> GetAllPostsFromUser(Guid userId)
        {
            var posts = await _postService.GetAllPostsFromUser(userId);

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found for this user.");
            }

            //Maak een lijst met Post exportmodels
            var postEMs = posts.Select(post => new PostEx
            {
                PostId = post.PostId,
                CreatedAt = post.CreatedAt,
                Rating = post.Rating,
                Review = post.Review,
                UserId = post.UserId,
                Title = post.Title

            }).ToList();

            return Ok(postEMs);
        }

        // GET: api/posts/{postId}
        [HttpGet("{postId}")]

        public async Task<ActionResult<IEnumerable<PostEx>>> GetPostById(Guid postId)
        {
            var post = await _postService.GetPostById(postId);

            if (post == null)
            {
                return NotFound();
            }

            // Zet de verkregen post om naar een PostEx-object
            var postEx = new PostEx
            {
                PostId = post.PostId,
                CreatedAt = post.CreatedAt,
                Rating = post.Rating,
                Review = post.Review,
                UserId = post.UserId,
                Title = post.Title
            };

            return Ok(postEx);
        }

        //POST: api/posts
        [HttpPost]

        public async Task<ActionResult<PostDto>> CreatePost(PostIm postIM)
        {
            Post post = new Post(postIM.Rating, postIM.Review, postIM.UserId, postIM.Title);

            await _postService.AddPost(post);

            // Zet de verkregen post om naar een PostEx-object
            var postEx = new PostEx
            {
                PostId = post.PostId,
                CreatedAt = post.CreatedAt,
                Rating = post.Rating,
                Review = post.Review,
                UserId = post.UserId,
                Title = post.Title
            };

            return CreatedAtAction(
            nameof(GetPostById),
            new { postId = post.PostId },
            postEx);

        }

        //PUT: api/posts/{postId}
        [HttpPut("{postId}")]
        public async Task<ActionResult> UpdatePost(Guid postId, PostIm postIM)
        {
            Post post = new Post(postId, postIM.CreatedAt, postIM.Rating, postIM.Review, postIM.UserId, postIM.Title);

            bool updated = await _postService.UpdatePost(post);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        //DELETE: api/posts/{postId}   
        [HttpDelete("{postId}")]
        public async Task<ActionResult> DeletePost(Guid postId)
        {
            var post = await _postService.GetPostById(postId);

            if (post == null)
            {
                return NotFound("Post not found.");
            }

            await _postService.DeletePost(postId);

            return NoContent();
        }

    }
}
