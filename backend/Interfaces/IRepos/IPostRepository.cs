namespace Interfaces.IRepos
{
    public interface IPostRepository
    {

        Task<Guid?> CreatePostAsync(PostDto dto);

        Task<PostDto?> GetPostByIdAsync(Guid postId);
        Task<IEnumerable<PostDto>> GetAllPostsFromUserAsync(Guid userId);
        Task<IEnumerable<PostDto>> GetPostsByCollectionIdAsync(Guid collectionId);

        Task<bool> UpdatePostAsync(PostDto dto);

        Task<bool> DeletePostAsync(Guid postId);

        Task AddPostToCollectionAsync(Guid collectionId, Guid postId);

        Task<bool> DeletePostFromCollectionAsync(Guid collectionId, Guid postId);
    }
}
