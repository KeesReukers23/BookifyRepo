using System.ComponentModel.DataAnnotations;

namespace Interfaces
{
    public class UserDto
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Password { get; set; }
        public ICollection<PostDto> Posts { get; set; } = null!;
        public ICollection<CommentDto> Comments { get; set; } = null!;

        public ICollection<LikeDto> Likes { get; set; } = null!;

        public ICollection<CollectionDto> Collections { get; set; } = null!;

    }
}
