using Interfaces;
using Logic.Entities;
using Logic.ExtensionMethods;

namespace Logic.Services
{
    public class CollectionService
    {

        private readonly ICollectionRepository _collectionRepository;


        public CollectionService(ICollectionRepository collectionRepository)
        {
            _collectionRepository = collectionRepository;
        }

        public async Task<Collection?> GetCollectionByIdAsync(Guid collectionId)
        {
            CollectionDto? dto = await _collectionRepository.GetByIdAsync(collectionId);
            if (dto == null) return null;
            return dto.ToCollection();
        }

        public async Task<IEnumerable<Collection>> GetAllCollectionsByUserIdAsync(Guid userId)
        {
            IEnumerable<CollectionDto> collectionsDtos = await _collectionRepository.GetAllByUserIdAsync(userId);

            IEnumerable<Collection> collections = collectionsDtos.Select(c => c.ToCollection());

            return collections;
        }

        public async Task<Collection> CreateCollectionAsync(string name, Guid userId)
        {
            Collection collection = new Collection(name, userId);
            await _collectionRepository.AddAsync(collection.toDto());
            return collection;
        }

        public async Task<Collection?> UpdateCollectionAsync(Guid collectionId, string newName)
        {
            CollectionDto dto = await _collectionRepository.GetByIdAsync(collectionId);
            if (dto == null) { return null; }

            dto.Name = newName;
            await _collectionRepository.UpdateAsync(dto);
            return dto.ToCollection();
        }

        public async Task<bool> DeleteCollectionAsync(Guid collectionId)
        {
            CollectionDto dto = await _collectionRepository.GetByIdAsync(collectionId);
            if (dto == null) return false;

            await _collectionRepository.RemoveAsync(collectionId);
            return true;
        }
    }
}
