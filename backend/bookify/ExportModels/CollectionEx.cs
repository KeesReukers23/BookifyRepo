using Logic.Entities;

namespace bookifyWEBApi.ExportModels
{
    public class CollectionEx
    {
        public Guid CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;

        public Guid UserId { get; set; }

        public List<PostCollection> PostCollections { get; set; } = new List<PostCollection>();
    }
}
