namespace bookifyWEBApi.ImportModels
{
    public class PostIm
    {
        public required float Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public required Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;

    }
}
