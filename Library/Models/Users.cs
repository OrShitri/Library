using Library.HelperClasses;

namespace Library.Models
{
    public class Users
    {
        #region Properties
        public string Name { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
        public bool UserApproved { get; set; }
        #endregion

        #region Constructor
        public Users(string name, string password)
        {
            Name = name;
            Password = password;
            UserType = UserType.User;
            UserApproved = true;
        }
        #endregion

        public override string ToString()
        {
            return Name.ToString() + " | Type: " + UserType;
        }
    }
}
