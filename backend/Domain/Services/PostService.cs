using Interfaces;
using Interfaces.IRepos;
using Logic.Entities;
using Logic.ExtensionMethods;

namespace Logic.Services
{
    public class PostService
    {

        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        public async Task<IEnumerable<Post>> GetPostsByCollectionIdAsync(Guid collectionId)
        {
            IEnumerable<PostDto> postDtos = await _postRepository.GetPostsByCollectionIdAsync(collectionId);
            if (postDtos == null)
            {
                return Enumerable.Empty<Post>();
            }

            IEnumerable<Post> posts = postDtos.Select(dto => dto.ToPost());
            return posts;
        }

        public async Task AddPostToCollectionAsync(Guid postId, Guid collectionId)
        {
            await _postRepository.AddPostToCollectionAsync(postId, collectionId);
        }
        public async Task<bool> DeletePostFromCollectionAsync(Guid collectionId, Guid postId)
        {
            bool success = await _postRepository.DeletePostFromCollectionAsync(collectionId, postId);
            return success;
        }


        public async Task<Guid?> AddPost(Post post)
        {
            PostDto dto = post.toDto();
            Guid? guid = await _postRepository.CreatePostAsync(dto);
            return guid;
        }
        public async Task<bool> DeletePost(Guid id)
        {
            bool deleted = false;

            deleted = await _postRepository.DeletePostAsync(id);

            return deleted;
        }
        public async Task<bool> UpdatePost(Post post)
        {
            bool result = false;
            PostDto dto = post.toDto();
            result = await _postRepository.UpdatePostAsync(dto);
            return result;
        }
        public async Task<Post?> GetPostById(Guid id)
        {
            PostDto? dto = await _postRepository.GetPostByIdAsync(id);
            if (dto == null)
            {
                return null;
            }
            return dto.ToPost();

        }
        public async Task<IEnumerable<Post>> GetAllPostsFromUser(Guid userId)
        {
            var postDtos = await _postRepository.GetAllPostsFromUserAsync(userId);
            if (postDtos == null)
            {
                return Enumerable.Empty<Post>();
            }

            var posts = postDtos.Select(dto => dto.ToPost());
            return posts;
        }



    }
}