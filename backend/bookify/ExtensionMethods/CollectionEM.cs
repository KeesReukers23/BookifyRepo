using bookifyWEBApi.ExportModels;
using Logic.Entities;

namespace bookifyWEBApi.ExtensionMethods
{
    public static class CollectionEM
    {
        public static CollectionEx ToCollectionEx(this Collection collection)
        {
            CollectionEx collectionEx = new CollectionEx
            {
                CollectionId = collection.CollectionId,
                Name = collection.Name,
                UserId = collection.UserId,
                PostCollections = collection.PostCollections
            };

            return collectionEx;
        }
    }
}
