using System.ComponentModel.DataAnnotations;

namespace Interfaces.Models
{
    public class CommentDto
    {
        [Key]
        public Guid CommentId { get; set; }
        public string CommentText { get; set; } = null!;

        public DateTime CreatedAt { get; set; }

        //FK to User
        public Guid UserId { get; set; }

        public UserDto User { get; set; } = null!;

        //FK to Post
        public Guid PostId { get; set; }
        public PostDto Post { get; set; } = null!;
    }
}
