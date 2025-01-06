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
                Posts = collection.Posts
                    .Select(post => new PostDto
                    {
                        PostId = post.PostId,
                        Title = post.Title,
                        Rating = post.Rating,
                        Review = post.Review,
                        UserId = post.UserId,
                        CreatedAt = post.CreatedAt
                    })
                    .ToList()
            };
            return dto;
        }

        public static Collection ToCollection(this CollectionDto dto)
        {
            var collection = new Collection(dto.CollectionId, dto.Name, dto.UserId);

            // Zet de PostCollections om van PostCollectionDto naar PostCollection
            if (dto.Posts != null)
            {
                foreach (var postDto in dto.Posts)
                {
                    Post post = new Post(postDto.PostId, postDto.CreatedAt, postDto.Rating, postDto.Review, postDto.UserId, postDto.Title);
                    collection.Posts.Add(post);
                }
            }
            return collection;
        }
    }
}
