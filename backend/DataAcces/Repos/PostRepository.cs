using DataAcces;
using Interfaces;
using Interfaces.IRepos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repos
{
    public class PostRepository : IPostRepository
    {

        private readonly BookifyContext _context;

        public PostRepository(BookifyContext context)
        {
            _context = context;
        }
        public async Task<Guid?> CreatePostAsync(PostDto dto)
        {
            try
            {
                await _context.Posts.AddAsync(dto);
                await _context.SaveChangesAsync();
                return dto.PostId;
            }
            catch
            {
                return null;
            }

        }
        public async Task<PostDto?> GetPostByIdAsync(Guid postId)
        {
            PostDto? dto = await _context.Posts.FindAsync(postId);
            return dto;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsFromUserAsync(Guid userId)
        {
            var postDtos = await _context.Posts
                                    .Where(x => x.UserId == userId)
                                    .ToListAsync();
            return postDtos;
        }

        public async Task<bool> UpdatePostAsync(PostDto dto)
        {
            PostDto? post = await _context.Posts.FindAsync(dto.PostId);

            if (post == null)
            {
                return false;
            }

            post.Rating = dto.Rating;
            post.Review = dto.Review;

            await _context.SaveChangesAsync();

            return true;

        }
        public async Task<bool> DeletePostAsync(Guid postId)
        {
            PostDto? dto = await _context.Posts.FindAsync(postId);

            if (dto == null)
            {
                return false;
            }

            _context.Posts.Remove(dto);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<PostDto>> GetPostsByCollectionIdAsync(Guid collectionId)
        {
            return await _context.PostCollections
            .Where(pc => pc.CollectionId == collectionId)
            .Select(pc => pc.Post)
            .ToListAsync();
        }
    }
}
