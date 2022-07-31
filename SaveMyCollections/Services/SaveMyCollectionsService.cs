using SaveMyCollectionsEnums;

namespace SaveMyCollections.Services
{
    public class SaveMyCollectionsService
    {
        public static IList<CollectionItemInfo> GetCollectionItems()
        {
            List<CollectionItemInfo> items = new List<CollectionItemInfo>();

            var bones = new CollectionItemInfo
            {
                MyColectionType = MyColectionType.Banknote,
                Name = "Banknotes",
                Description= "BanknotesCollectionDescription",
                ImageUrl = "../Images/Banknotes.jpg",
                CollectionUrl = "/Collections/Banknotes/Index"
            };
            var coins =  new CollectionItemInfo
            {
                MyColectionType = MyColectionType.Coin,
                Name = "Coins",
                Description = "CoinsCollectionDescription",
                ImageUrl = "../Images/Coins.jpg",
                CollectionUrl = "/Collections/Coins/Index"
            };
            var stamps = new CollectionItemInfo
            {
                MyColectionType = MyColectionType.Stamp,
                Name = "Stamps",
                Description = "StampsCollectionDescription",
                ImageUrl = "../Images/Stamps.jpg",
                CollectionUrl = "/Collections/Stamps/Index"
            };

            items.Add(bones);
            items.Add(coins);
            items.Add(stamps);
            return items;            

        }
                

        public class CollectionItemInfo
        {
            public MyColectionType MyColectionType { get; set; }
            public string Name { get;set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public string CollectionUrl { get; set; }

        }
            
    }
}
