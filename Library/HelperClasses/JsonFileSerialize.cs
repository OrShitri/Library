using Library.Models;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Library.HelperClasses
{
    public static class JsonFileSerialize
    {
        public static void SimpleWrite(MyCashBox cashBox, List<Users> users, List<Item> items)
        {
            LibraryData libraryDataSave = new LibraryData();

            List<Book> tempListBook = new List<Book>();
            List<Magazine> tempListMagazine = new List<Magazine>();

            foreach (var item in items)
            {
                if (item.ItemType == ItemType.Book)
                    tempListBook.Add((Book)item);
                else
                    tempListMagazine.Add((Magazine)item);
            }

            libraryDataSave.CashBox = cashBox;
            libraryDataSave.Users = users;
            libraryDataSave.Books = tempListBook;
            libraryDataSave.Magazines = tempListMagazine;

            string jsonString = JsonSerializer.Serialize(libraryDataSave);
            File.WriteAllText("MyFile.json", jsonString);
        }

        public static void SimpleRead(ref MyCashBox cashBox, ref List<Users> users, ref List<Item> items)
        {
            LibraryData libraryDataLoad = new LibraryData();

            if (File.Exists("MyFile.json"))
            {
                string fileName = "MyFile.json";
                string jsonString = File.ReadAllText(fileName);

                libraryDataLoad = JsonSerializer.Deserialize<LibraryData>(jsonString)!;

                List<Item> tempList = new List<Item>();

                tempList.AddRange(libraryDataLoad.Books!);
                tempList.AddRange(libraryDataLoad.Magazines!);

                cashBox = libraryDataLoad.CashBox!;
                users = libraryDataLoad.Users!;
                items = tempList;
            }
        }
    }
}
