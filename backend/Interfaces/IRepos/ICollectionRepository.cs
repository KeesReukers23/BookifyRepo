namespace Interfaces.IRepos
{
    public interface ICollectionRepository
    {
        Task<CollectionDto?> GetByIdAsync(Guid collectionId);
        Task<IEnumerable<CollectionDto>> GetAllAsync();

        Task<IEnumerable<CollectionDto>> GetAllByUserIdAsync(Guid userId);
        Task AddAsync(CollectionDto dto);
        Task UpdateAsync(CollectionDto dto);
        Task RemoveAsync(Guid collectionId);
    }

}
