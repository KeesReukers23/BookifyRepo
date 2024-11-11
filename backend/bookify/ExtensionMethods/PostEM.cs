using bookifyWEBApi.ImportModels;
using Logic.Entities;

namespace bookifyWEBApi.ExtensionMethods
{
    public static class PostEM
    {
        public static Post ToPost(this PostIM postIM)
        {
            Post post = new Post(postIM.Rating, postIM.Review, postIM.UserId, postIM.Title);

            return post;
        }
    }
}
