using Library.HelperClasses;
using System;

namespace Library.Models
{
    public class Magazine : Item
    {
        #region Constructor
        public Magazine(int id, ItemType itemType, string name, string genre, string publisher,
            double regularPrice, DateTime publishDate)
            : base(id, itemType, name, genre, publisher, regularPrice, publishDate)
        {

        }
        #endregion

        public override string ToString()
        {
            return ID.ToString() + " " + Name + " " + Genre + " " + Publisher + " " +
                RegularPrice.ToString("F") + " " + DiscountPrice.ToString("F") + " % " +
                CurrentPrice.ToString("F") + " " + PublishDate.ToString("dd,MM,yyyy HH:mm");
        }
    }
}
