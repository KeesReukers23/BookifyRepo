namespace bookifyWEBApi.ImportModels
{
    public class AddToCollectionIm
    {
        public required Guid CollectionId { get; set; }
        public required Guid PostId { get; set; }
    }
}
