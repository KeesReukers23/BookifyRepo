using Interfaces;

namespace Logic.Services
{
    public class PostCollectionService
    {
        private readonly IPostCollectionRepository _postCollectionRepository;
        private readonly ICollectionRepository _collectionRepository;

        public PostCollectionService(IPostCollectionRepository postCollectionRepository, ICollectionRepository collectionRepository)
        {
            _postCollectionRepository = postCollectionRepository;
            _collectionRepository = collectionRepository;
        }

        public async Task<bool> AddPostToCollectionAsync(Guid collectionId, Guid postId)
        {
            var existingPostCollection = await _postCollectionRepository.GetPostCollectionAsync(collectionId, postId);

            if (existingPostCollection != null)
            {
                return false;
            }

            PostCollectionDto postCollection = new PostCollectionDto()
            {
                PostId = postId,
                CollectionId = collectionId
            };

            await _postCollectionRepository.AddAsync(postCollection);
            return true;
        }

        public async Task RemovePostFromCollectionAsync(Guid collectionId, Guid postId)
        {
            await _postCollectionRepository.RemovePostFromCollectionAsync(collectionId, postId);
        }
    }
}
