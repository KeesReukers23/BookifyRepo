namespace bookifyWEBApi.ImportModels
{
    public class CollectionIm
    {
        public string Name { get; set; } = string.Empty;

        public required Guid UserId { get; set; }
    }
}
