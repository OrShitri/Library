using Library.HelperClasses;

namespace Library.Models
{
    public class Librarian : Users
    {
        #region Constructor
        public Librarian(string name, string password) : base(name, password)
        {
            UserType = UserType.Librarian;
            UserApproved = false;
        }
        #endregion 

        public override string ToString()
        {
            return Name!.ToString() + " | Type: " + UserType;
        }
    }
}
