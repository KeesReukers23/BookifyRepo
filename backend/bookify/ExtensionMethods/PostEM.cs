using bookifyWEBApi.ExportModels;
using bookifyWEBApi.ImportModels;
using Logic.Entities;

namespace bookifyWEBApi.ExtensionMethods
{
    public static class PostEm
    {
        public static Post ToPost(this PostIm postIM)
        {
            Post post = new Post(postIM.Rating, postIM.Review, postIM.UserId, postIM.Title);

            return post;
        }

        public static PostEx toPostEx(this Post post)
        {

            PostEx postEx = new PostEx()
            {
                PostId = post.PostId,
                CreatedAt = post.CreatedAt,
                Rating = post.Rating,
                Review = post.Review,
                UserId = post.UserId,
                Title = post.Title
            };
            return postEx;

        }
    }
}
