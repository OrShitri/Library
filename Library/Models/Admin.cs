using Library.HelperClasses;

namespace Library.Models
{
    public class Admin :Users
    {
        #region Constructor
        public Admin(string name, string password) : base(name, password)
        {
            UserType = UserType.Admin;
        }
        #endregion 

        public override string ToString()
        {
            return Name!.ToString() + " | Type: " + UserType;
        }
    }
}
