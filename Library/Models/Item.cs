using Library.HelperClasses;
using System;

namespace Library.Models
{
    public abstract class Item
    {
        #region Properties
        public double CurrentPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string? Genre { get; set; }
        public int ID { get; set; }
        public ItemType ItemType { get; set; }
        public bool LateOnReturn { get; set; }
        public string? Name { get; set; }
        public DateTime PublishDate { get; set; }
        public string? Publisher { get; set; }
        public double RegularPrice { get; set; }
        public DateTime RentDate { get; set; }
        public string? Renter { get; set; }
        public DateTime ReturnDate { get; set; }
        #endregion

        #region Constructor
        public Item(int id, ItemType itemType, string name, string genre, string publisher,
            double regularPrice, DateTime publishDate)
        {
            ID = id;
            ItemType = itemType;
            Name = name;
            Genre = genre;
            Publisher = publisher;
            RegularPrice = regularPrice;
            CurrentPrice = GetCurrentprice();
            PublishDate = publishDate;
        }
        #endregion

        public double GetCurrentprice()
        {
            return RegularPrice - RegularPrice * (DiscountPrice / 100);
        }

        public abstract override string ToString();

    }
}
