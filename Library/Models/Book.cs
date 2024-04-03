using Library.HelperClasses;
using System;

namespace Library.Models
{
    public class Book : Item
    {
        #region Properties
        public string Author { get; set; }
        #endregion

        #region Constructor
        public Book(int id, ItemType itemType, string name, string genre, string author,
            string publisher, double regularPrice, DateTime publishDate)
            : base(id, itemType, name, genre, publisher, regularPrice, publishDate)
        {
            Author = author;
        }
        #endregion

        public override string ToString()
        {
            return ID.ToString() + " " + Name + " " + Genre + " " + Author + " " + Publisher + " " +
                RegularPrice.ToString("F") + " " + DiscountPrice.ToString("F") + " % " +
                CurrentPrice.ToString("F") + " " + PublishDate.ToString("dd,MM,yyyy HH:mm");
        }
    }
}
