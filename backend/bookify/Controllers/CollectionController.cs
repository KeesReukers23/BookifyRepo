using bookifyWEBApi.ExportModels;
using bookifyWEBApi.ExtensionMethods;
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
        [HttpGet("{id}")]
        public async Task<ActionResult<CollectionEx>> GetCollectionByIdAsync(Guid id)
        {
            Collection collection = await _collectionService.GetCollectionByIdAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            return Ok(collection.ToCollectionEx());
        }

        //GET: api/Collection/user/{userId} 
        [HttpGet("byUser/{userId}")]
        public async Task<ActionResult<IEnumerable<CollectionEx>>> GetCollectionsByUserIdAsync(Guid userId)
        {
            var collections = await _collectionService.GetAllCollectionsByUserIdAsync(userId);
            if (collections == null || !collections.Any())
            {
                return NotFound("No collections found for this user.");
            }

            var collectionEMs = collections.Select(collection => collection.ToCollectionEx()).ToList();
            return Ok(collectionEMs);
        }

        //POST: api/Collection
        [HttpPost]
        public async Task<ActionResult<CollectionEx>> CreateCollectionAsync([FromBody] CollectionIM collectionIM)
        {
            if (collectionIM == null || string.IsNullOrWhiteSpace(collectionIM.Name))
            {
                return BadRequest("Collection name cannot be empty.");
            }

            Collection collection = await _collectionService.CreateCollectionAsync(collectionIM.Name, collectionIM.UserId);
            return CreatedAtAction(nameof(GetCollectionByIdAsync), new { id = collection.CollectionId }, collection.ToCollectionEx());
        }

        //DELETE: api/Collection/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollectionAsync(Guid id)
        {
            bool success = await _collectionService.DeleteCollectionAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
