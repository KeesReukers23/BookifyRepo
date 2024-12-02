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
        public string? Review { get; set; }

        public string Title { get; set; }

        //FK to User
        public Guid UserId { get; set; }

        [JsonIgnore]
        public UserDto User { get; set; }

        public ICollection<CommentDto> Comments { get; set; } = null!;

        public ICollection<LikeDto> Likes { get; set; } = null!;

        public ICollection<PostCollectionDto> PostCollections { get; set; } = new List<PostCollectionDto>();
    }
}
