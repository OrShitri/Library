namespace Library.HelperClasses
{
    public static class ErrorMessages
    {
        #region Error In Add New User
        public static string EmptyFieldsError()
        {
            return "All fields must be filled";
        }

        public static string UsernameInvalidError()
        {
            return "Username must contain letters and numbers with a minimum length of 4 characters";
        }

        public static string UsernameExistsError()
        {
            return "Username already exists";
        }

        public static string PasswordInvalidError()
        {
            return "Password must be at least 6 characters long";
        }

        public static string ConfirmPasswordError()
        {
            return "The passwords do not match each other";
        }
        #endregion

        #region Error In Login
        public static string WrongLoginError()
        {
            return "The username or password is incorrect";
        }

        public static string LibrarianAccountNotApproved()
        {
            return "Your account has not yet been approved by the system administrator.";
        }

        public static string UserAccountBlocked()
        {
            return "Your account has been blocked. Contact your system administrator to access the account.";
        }
        #endregion

        #region Error In Add/Edit Item
        public static string FirstCharacterIsSpace()
        {
            return "In none of the fields the first character can be a space";
        }

        public static string ItemSelectedError()
        {
            return "No item selected, you must select a Book or Magazine";
        }

        public static string InvalidRangeOfDateError()
        {
            return "The time ranges of the date are:\nDay: 01 - 31\nMonth: 01 - 12\nYear: 2000+ ";
        }

        public static string InvalidDateError()
        {
            return "The date entered is incorrect";
        }

        public static string FutureDateError()
        {
            return "The date you are trying to enter is a future date";
        }

        public static string BookWithSameNameError()
        {
            return "It is not possible to add the same book name\nby the same author";
        }

        public static string MagazineWithSameNameError()
        {
            return "It is not possible to add a magazine\nwith the same name";

        }
        #endregion
    }
}
