using System.ComponentModel.DataAnnotations;

namespace Interfaces.Models
{
    public class UserDto
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public ICollection<PostDto> Posts { get; set; } = null!;
        public ICollection<CommentDto> Comments { get; set; } = null!;

        public ICollection<LikeDto> Likes { get; set; } = null!;

    }
}
