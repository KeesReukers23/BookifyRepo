namespace Interfaces.IRepos
{
    public interface IPostRepository
    {

        Task<Guid?> CreatePostAsync(PostDto dto);

        Task<PostDto?> GetPostByIdAsync(Guid postId);
        Task<IEnumerable<PostDto>> GetAllPostsFromUserAsync(Guid UserId);
        Task<IEnumerable<PostDto>> GetPostsByCollectionIdAsync(Guid collectionId);

        Task<bool> UpdatePostAsync(PostDto dto);

        Task<bool> DeletePostAsync(Guid postId);
    }
}
