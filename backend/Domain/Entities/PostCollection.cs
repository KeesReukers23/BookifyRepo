namespace Logic.Entities
{
    public class PostCollection
    {
        public Guid PostId { get; }
        public Guid CollectionId { get; }

        public PostCollection(Guid postId, Guid collectionId)
        {
            PostId = postId;
            CollectionId = collectionId;
        }
    }
}
