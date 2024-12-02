using System.ComponentModel.DataAnnotations;

namespace Interfaces
{
    public class CollectionDto
    {
        [Key]
        public Guid CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public UserDto user { get; set; } = null!;

        public ICollection<PostCollectionDto> PostCollections { get; set; } = new List<PostCollectionDto>();
    }
}
