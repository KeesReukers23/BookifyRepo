using Interfaces;
using Logic.Entities;

namespace Logic.ExtensionMethods
{
    public static class PostEM
    {
        public static PostDto toDto(this Post post)
        {
            PostDto dto = new PostDto()
            {
                PostId = post.PostId,
                CreatedAt = post.CreatedAt,
                Rating = post.Rating,
                Review = post.Review,
                UserId = post.UserId,
                Title = post.Title
            };
            return dto;
        }

        public static Post ToPost(this PostDto dto)
        {
            Post post = new Post(dto.PostId, dto.CreatedAt, dto.Rating, dto.Review, dto.UserId, dto.Title);
            return post;
        }
    }
}
