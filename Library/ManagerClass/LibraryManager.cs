using Library.HelperClasses;
using Library.Models;
using System;
using System.Collections.Generic;

namespace Library.ManagerClass
{
    public class LibraryManager
    {
        private List<Users> users;
        private List<Item> items;
        private MyCashBox myCashBox;
        private int rentalDays = 30;
        public double LibraryCashBox { get; set; }


        public LibraryManager()
        {
            users = new List<Users>();
            items = new List<Item>();
            myCashBox = new MyCashBox();

            CreateDefault();
            GetFromJsonFile();
            SortByID();
        }

        public void AddDiscount(int id, double discountPrice)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            item.DiscountPrice = discountPrice;
            item.CurrentPrice = item.GetCurrentprice();
        }

        public void AddItem(ItemType itemType, string name, string genre, string author, string publisher, double price, DateTime publishDate)
        {
            int id = CraetNewID();

            if (itemType == ItemType.Book)
                items.Add(new Book(id, ItemType.Book, name, genre, author, publisher, price, publishDate));

            else
                items.Add(new Magazine(id, ItemType.Magazine, name, genre, publisher, price, publishDate));
        }

        private int CraetNewID()
        {
            int id = 0;
            Item? item;

            if (items.Count > 0)
            {
                item = items[items.Count - 1];
                id = item.ID;
            }

            return id + 1;
        }

        private void SortByID()
        {
            items.Sort((x, y) => x.ID.CompareTo(y.ID));
        }

        public bool CheckNameOfNewItem(ItemType itemType, string itemName, string itemAuthor, int currentID)
        {
            Item? item;

            if (itemType == ItemType.Book)
                item = items.Find(x => x is Book && x.Name == itemName && (x as Book)!.Author == itemAuthor && x.ID != currentID);

            else
                item = items.Find(x => x is Magazine && x.Name == itemName && x.ID != currentID);

            if (item == null)
                return false;

            return true;
        }

        public void AddNewUser(string username, string password, UserType userType)
        {
            if (userType == UserType.User)
                users.Add(new Users(username, password));

            else
                users.Add(new Librarian(username, password));
        }

        public void AddToJsonFile()
        {
            JsonFileSerialize.SimpleWrite(myCashBox, users, items);
        }

        public void CreateDefault()
        {
            if (System.IO.File.Exists("MyFile.json"))
                return;

            else
            {
                users.Add(new Admin("AdminUser@1991", "123456")); 

                users.Add(new Librarian("SuperUser@1991", "123456"));
                users[1].UserApproved = true;

                JsonFileSerialize.SimpleWrite(myCashBox, users, items);
            }
        }

        public void EditeItem(int id, string name, string genre, string author, string publisher, double price, DateTime publishDate)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            item.Name = name;
            item.Genre = genre;
            item.Publisher = publisher;
            item.RegularPrice = price;
            item.CurrentPrice = item.GetCurrentprice();
            item.PublishDate = publishDate;

            if (item is Book)
                (item as Book)!.Author = author;
        }

        public List<string> ReturnDetailsOfSelectedItem(int id)
        {
            List<string> returnDetails = new List<string>();

            Item item;
            item = items.Find(x => x.ID == id)!;

            returnDetails.Add(item.ItemType!.ToString());
            returnDetails.Add(item.Name!);
            returnDetails.Add(item.Genre!);
            returnDetails.Add(item.Publisher!);
            returnDetails.Add(item.RegularPrice.ToString());
            returnDetails.Add(item.PublishDate.Day.ToString());
            returnDetails.Add(item.PublishDate.Month.ToString());
            returnDetails.Add(item.PublishDate.Year.ToString());

            if (item is Book)
                returnDetails.Add((item as Book)!.Author);

            return returnDetails;
        }

        public List<Users> GetUserList()
        {
            List<Users> userList = new List<Users>();

            foreach (var user in users)
            {
                userList.Add(user);
            }

            return userList;
        }

        public List<Item> GetAllItemsInLibrary()
        {
            List<Item> itemsInLibrarylist = new List<Item>();

            foreach (var item in items)
            {
                if (item.RentDate == DateTime.MinValue)
                    itemsInLibrarylist.Add(item);
            }

            return itemsInLibrarylist;
        }

        public List<Item> GetAllItemsInRent()
        {
            List<Item> itemsInRentlist = new List<Item>();

            foreach (var item in items)
            {
                if (item.RentDate > DateTime.MinValue)
                    itemsInRentlist.Add(item);
            }

            return itemsInRentlist;
        }

        public void GetFromJsonFile()
        {
            JsonFileSerialize.SimpleRead(ref myCashBox, ref users, ref items);

            LibraryCashBox = myCashBox.TotalCash;
        }

        public void LateOnReturn()
        {
            foreach (var item in items)
            {
                if (item.Renter != null && item.RentDate <= DateTime.Now.AddDays(-rentalDays))
                    item.LateOnReturn = true;
            }
        }

        public UserType UserTypeCheck(string username)
        {
            var user = users.Find(x => x.Name == username);

            return user!.UserType!;
        }

        public void RemoveDiscount(int id)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            item.DiscountPrice = 0;
            item.CurrentPrice = item.RegularPrice;
        }

        public void RemoveItem(int id)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            items.Remove(item);
        }

        public void RemoveUser(string username)
        {
            Users user;
            user = users.Find(x => x.Name == username)!;

            users.Remove(user);
        }

        public void ApproveUser(string username)
        {
            Users user;
            user = users.Find(x => x.Name == username)!;

            user.UserApproved = !user.UserApproved;
        }

        public void RentItem(int id, string username)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            item.RentDate = DateTime.Now;
            item.ReturnDate = DateTime.Now.AddDays(rentalDays);
            item.Renter = username;

            myCashBox.AddCash(item.CurrentPrice);

            LibraryCashBox = myCashBox.TotalCash;
        }

        public void ReturnItem(int id)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            item.RentDate = DateTime.MinValue;
            item.ReturnDate = DateTime.MinValue;
            item.LateOnReturn = false;
            item.Renter = null;
        }

        public List<Item> Search(string type, string name, string genre, string author, string publisher, string price)
        {
            List<Item> searchResults = new List<Item>();

            foreach (var item in items)
            {
                if (item is Book)
                {
                    if (item.ItemType.ToString().Contains(type) && item.Name!.Contains(name) && item.Genre!.Contains(genre) &&
                        (item as Book)!.Author.Contains(author) && item.Publisher!.Contains(publisher) &&
                        item.CurrentPrice.ToString().StartsWith(price) && item.RentDate == DateTime.MinValue)

                        searchResults.Add(item);
                }
                else
                {
                    if (item.ItemType!.ToString().Contains(type) && item.Name!.Contains(name) && item.Genre!.Contains(genre) &&
                        author == "" && item.Publisher!.Contains(publisher) &&
                        item.CurrentPrice.ToString().StartsWith(price) && item.RentDate == DateTime.MinValue)

                        searchResults.Add(item);
                }
            }

            return searchResults;
        }

        public bool UserCheck(string username)
        {
            var user = users.Find(x => x.Name == username);

            if (user == null)
                return false;

            return true;
        }

        public bool UserApproveCheck(string username)
        {
            var user = users.Find(x => x.Name == username);

            if (user!.UserApproved == false)
                return false;

            return true;
        }

        public bool PasswordIsCorrect(string username, string password)
        {
            var user = users.Find(x => x.Name == username);

            if (user!.Password == password)
                return true;

            return false;
        }

        public DateTime CheckPublisheDate(int day, int month, int year)
        {
            DateTime publishDate;
            try
            {
                publishDate = new DateTime(year, month, day);
                return publishDate;
            }
            catch (ArgumentOutOfRangeException)
            {
                ErrorMessages.InvalidDateError();
                return DateTime.MinValue;
            }
        }

        public int GetCountOfAllItemInLibraray()
        {
            return items.Count;
        }

        public int GetCountOfAllBookInLibraray()
        {
            int counter = 0;
            foreach (var item in items)
            {
                if (item.ItemType == ItemType.Book)
                    counter++;
            }

            return counter;
        }

        public int GetCountOfAllItemsAvailable()
        {
            List<Item> itemsInLibrarylist = new List<Item>();
            itemsInLibrarylist = GetAllItemsInLibrary();

            return itemsInLibrarylist.Count;
        }

        public string GetRenterOfItem(int id)
        {
            Item item;
            item = items.Find(x => x.ID == id)!;

            return item.Renter!;
        }

        public string GetUsernameOfAdmin()
        {
            return users[0].Name;
        }

        public string GetUsernameOfSuperUser() 
        {
            return users[1].Name;
        }

    }
}
