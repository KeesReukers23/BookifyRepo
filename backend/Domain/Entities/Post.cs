namespace Logic.Entities
{
    public class Post
    {
        public Guid PostId { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public float Rating { get; set; }
        public string Review { get; set; }
        public Guid UserId { get; private set; }
        public string Title { get; set; }

        public ICollection<Collection> Collections { get; set; }

        //Constructor for creating a new Post
        public Post(float rating, string review, Guid userId, string title)
        {
            this.PostId = Guid.NewGuid();
            this.CreatedAt = DateTime.Now;
            this.Rating = rating;
            this.Review = review;
            this.UserId = userId;
            this.Title = title;
            this.Collections = new List<Collection>();
        }

        //Constructor for retrieving a Post
        public Post(Guid postId, DateTime createdAt, float rating, string review, Guid userId, string title)
        {
            this.PostId = postId;
            this.CreatedAt = createdAt;
            this.Rating = rating;
            this.Review = review;
            this.UserId = userId;
            this.Title = title;
            this.Collections = new List<Collection>();
        }
    }
}
