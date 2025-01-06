using Interfaces;
public interface ICollectionRepository
{
    Task<CollectionDto?> GetByIdAsync(Guid collectionId);
    Task<IEnumerable<CollectionDto>> GetAllAsync();

    Task<IEnumerable<CollectionDto>> GetAllByUserIdAsync(Guid userId);
    Task AddAsync(CollectionDto collection);
    Task UpdateAsync(CollectionDto collection);
    Task RemoveAsync(Guid collectionId);
}