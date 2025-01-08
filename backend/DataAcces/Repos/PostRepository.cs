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
            var posts = await _context.Collections
                .Where(c => c.CollectionId == collectionId)
                .SelectMany(c => c.Posts)
                .ToListAsync();

            return posts;
        }

        public async Task AddPostToCollectionAsync(Guid collectionId, Guid postId)
        {
            var collection = await _context.Collections.FindAsync(collectionId);

            var post = await _context.Posts.FindAsync(postId);

            if (collection == null || post == null)
            {
                throw new ArgumentException("Collection or Post not found.");
            }

            collection.Posts.Add(post);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePostFromCollectionAsync(Guid collectionId, Guid postId)
        {
            var collection = await _context.Collections
                .Include(c => c.Posts)
                .FirstOrDefaultAsync(c => c.CollectionId == collectionId);

            var post = await _context.Posts.FindAsync(postId);

            if (collection == null || post == null)
            {
                throw new ArgumentException("Collection or Post not found.");
            }

            collection.Posts.Remove(post);

            _context.Entry(collection).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return !collection.Posts.Contains(post);
        }
    }
}
