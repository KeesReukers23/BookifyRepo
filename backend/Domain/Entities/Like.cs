namespace Logic.Entities
{
    public class Like
    {
        public Guid LikeId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid PostId { get; private set; }

        //Constructor for creating a Like
        public Like(Guid userId, Guid postId)
        {
            this.LikeId = Guid.NewGuid();
            this.UserId = userId;
            this.PostId = postId;
        }

        //Constructor for retrieving like
        public Like(Guid likeId, Guid userId, Guid postId)
        {
            this.LikeId = likeId;
            this.UserId = userId;
            this.PostId = postId;
        }
    }
}
