namespace bookifyWEBApi.ExportModels
{
    public class PostEx
    {
        public Guid PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Title { get; set; }
    }
}
