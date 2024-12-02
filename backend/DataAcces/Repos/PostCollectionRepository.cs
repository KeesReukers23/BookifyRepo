using Interfaces;

namespace DataAccess.Repos
{
    public class PostCollectionRepository : IPostCollectionRepository
    {
        public Task AddAsync(PostCollectionDto postCollection)
        {
            throw new NotImplementedException();
        }

        public Task<PostCollectionDto> GetPostCollectionAsync(Guid collectionId, Guid postId)
        {
            throw new NotImplementedException();
        }

        public Task RemovePostFromCollectionAsync(Guid collectionId, Guid postId)
        {
            throw new NotImplementedException();
        }
    }
}
