using Interfaces;
using Logic.Entities;

namespace Logic.ExtensionMethods
{
    public static class PostCollectionEM
    {
        public static PostCollectionDto toDto(this PostCollection postCollection)
        {
            PostCollectionDto dto = new PostCollectionDto()
            {
                PostId = postCollection.PostId,
                CollectionId = postCollection.CollectionId
            };

            return dto;
        }


    }
}
