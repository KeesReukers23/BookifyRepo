namespace Logic.Entities
{
    public class Collection
    {
        public Guid CollectionId { get; private set; }
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; private set; }

        public ICollection<PostCollection> PostCollections { get; set; }

        public Collection(string name, Guid userId)
        {
            CollectionId = Guid.NewGuid();
            Name = name;
            UserId = userId;
            PostCollections = new List<PostCollection>();
        }

        public Collection(Guid collectionId, string name, Guid userId)
        {
            CollectionId = collectionId;
            Name = name;
            UserId = userId;
            PostCollections = new List<PostCollection>();
        }
    }
}
