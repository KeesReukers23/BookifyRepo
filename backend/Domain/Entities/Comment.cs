namespace Logic.Entities
{
    public class Comment
    {
        public Guid CommentId { get; private set; }
        public string CommentText { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid UserId { get; private set; }

        //Constructor for creating a Comment
        public Comment(string commentText, Guid userId)
        {
            this.CommentId = Guid.NewGuid();
            this.CommentText = commentText;
            this.UserId = userId;
        }

        //Constructor for retrieving a Comment
        public Comment(Guid commentId, string commentText, DateTime createdAt, Guid userId)
        {
            this.CommentId = commentId;
            this.CommentText = commentText;
            this.CreatedAt = createdAt;
            this.UserId = userId;
        }

    }
}
