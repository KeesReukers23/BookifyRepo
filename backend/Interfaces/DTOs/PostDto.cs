using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Interfaces
{
    public class PostDto
    {
        [Key]
        public Guid PostId { get; set; }

        public DateTime CreatedAt { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        //FK to User
        public Guid UserId { get; set; }

        [JsonIgnore]
        public UserDto? User { get; set; }

        public ICollection<CommentDto> Comments { get; set; } = new List<CommentDto>();

        public ICollection<LikeDto> Likes { get; set; } = new List<LikeDto>();

        public ICollection<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
    }
}
