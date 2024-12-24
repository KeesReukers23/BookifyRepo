using System.ComponentModel.DataAnnotations;

namespace Interfaces
{
    public class CollectionDto
    {
        [Key]
        public Guid CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public UserDto User { get; set; } = null!;

        public ICollection<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}
