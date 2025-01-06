using bookifyWEBApi.ImportModels;
using Logic.Entities;
using Logic.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace bookifyWEBApi.Controllers
{
    [Authorize] //Jwt token check
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        private readonly CollectionService _collectionService;
        private readonly PostService _postService;
        public CollectionController(CollectionService collectionService, PostService postService)
        {
            _collectionService = collectionService;
            _postService = postService;
        }

        // GET: api/Collection/{id}
        [HttpGet("{collectionId}", Name = "GetCollectionById")]
        public async Task<ActionResult<Collection>> GetCollectionByIdAsync(Guid collectionId)
        {
            Collection? collection = await _collectionService.GetCollectionByIdAsync(collectionId);

            if (collection == null)
            {
                return NotFound();
            }

            return Ok(collection);
        }

        //GET: api/Collection/user/{userId} 
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollectionsByUserIdAsync(Guid userId)
        {
            var collections = await _collectionService.GetAllCollectionsByUserIdAsync(userId);
            if (collections == null || !collections.Any())
            {
                return NotFound("No collections found for this user.");
            }

            return Ok(collections);
        }

        // GET: api/Collection/{id}/posts
        [HttpGet("{collectionId}/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsByCollectionId(Guid CollectionId)
        {
            var posts = await _postService.GetPostsByCollectionIdAsync(CollectionId);

            if (posts == null || !posts.Any())
            {
                return NotFound("No posts found for this collection.");
            }

            return Ok(posts);
        }

        //POST: api/Collection
        [HttpPost]
        public async Task<ActionResult<Collection>> CreateCollectionAsync([FromBody] CollectionIm collectionIm)
        {
            if (collectionIm == null || string.IsNullOrWhiteSpace(collectionIm.Name))
            {
                return BadRequest("Collection name cannot be empty.");
            }

            Collection collection = await _collectionService.CreateCollectionAsync(collectionIm.Name, collectionIm.UserId);

            // Genereer de URL van de collectie
            string? url = Url.RouteUrl("GetCollectionById", new { collectionId = collection.CollectionId });

            if (url == null)
            {
                return StatusCode(500, "Error generating the collection URL.");
            }

            // Geef de URL van de collectie en de collectie zelf terug
            return Created(url, collection);
        }

        // POST api/Collection/AddPostToCollection
        [HttpPost("{collectionId}/post")]
        public async Task<IActionResult> AddPostToCollection([FromBody] AddToCollectionIm request)
        {
            if (request == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                // Voeg de post toe aan de collectie via de service
                await _postService.AddPostToCollectionAsync(request.CollectionId, request.PostId);
                return Ok("Post added to collection successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        //DELETE: api/Collection/{id}
        [HttpDelete("{collectionId}")]
        public async Task<IActionResult> DeleteCollectionAsync(Guid collectionId)
        {
            bool success = await _collectionService.DeleteCollectionAsync(collectionId);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}
