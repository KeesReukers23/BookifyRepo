using Interfaces;
using Logic.Entities;

namespace Logic.ExtensionMethods
{
    public static class CollectionEM
    {
        public static CollectionDto toDto(this Collection collection)
        {
            CollectionDto dto = new CollectionDto()
            {
                CollectionId = collection.CollectionId,
                UserId = collection.UserId,
                Name = collection.Name,
                PostCollections = collection.PostCollections
                    .Select(postCollection => new PostCollectionDto
                    {
                        PostId = postCollection.PostId,
                        CollectionId = postCollection.CollectionId
                    })
                    .ToList()
            };
            return dto;
        }

        public static Collection ToCollection(this CollectionDto dto)
        {
            var collection = new Collection(dto.CollectionId, dto.Name, dto.UserId);

            // Zet de PostCollections om van PostCollectionDto naar PostCollection
            if (dto.PostCollections != null)
            {
                foreach (var postCollectionDto in dto.PostCollections)
                {
                    collection.PostCollections.Add(new PostCollection(postCollectionDto.PostId, postCollectionDto.CollectionId));
                }
            }

            return collection;
        }
    }
}
