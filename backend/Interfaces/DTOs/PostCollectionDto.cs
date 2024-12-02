namespace Interfaces
{
    public class PostCollectionDto
    {
        public Guid CollectionId { get; set; }  // FK naar Collection                
        public CollectionDto Collection { get; set; } = null!;

        public Guid PostId { get; set; }  // FK naar Post                
        public PostDto Post { get; set; } = null!;
    }
}
