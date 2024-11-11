namespace bookifyWEBApi.ImportModels
{
    public class PostIM
    {
        public DateTime CreatedAt { get; set; }
        public float Rating { get; set; }
        public string? Review { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }

    }
}
