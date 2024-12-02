using Interfaces;

public interface IPostCollectionRepository
{
    Task<PostCollectionDto> GetPostCollectionAsync(Guid collectionId, Guid postId);
    Task AddAsync(PostCollectionDto postCollection);
    Task RemovePostFromCollectionAsync(Guid collectionId, Guid postId);
}
