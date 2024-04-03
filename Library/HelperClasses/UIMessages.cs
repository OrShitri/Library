using System.Windows;

namespace Library.HelperClasses
{
    public static class UIMessages
    {
        #region Info Message SignUp
        public static void AddUserMessage()
        {
            string title = "Add User";
            string textMessage = "New user has been successfully added";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void AddLibrarianMessage()
        {
            string title = "Add Librarian";
            string textMessage = "New user has been successfully added";
            MessageBoxViewInfo(title, textMessage);
        }
        #endregion

        #region Message Add/Edite Item
        public static string AddItemMessage()
        {
            return "New item has been successfully added";
        }

        public static string EditeItemMessage()
        {
            return "Item edited successfully";
        }
        #endregion

        #region Information notice In Menus
        public static void EditeItemInfoMessage()
        {
            string title = "Edite Item";
            string textMessage = "To edit an item you must first select an item from the list of items in the library";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void RemoveItemInfoMessage()
        {
            string title = "Remove Item";
            string textMessage = "To remove an item you must first select an item from the list of library items";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void AddDiscountItemInfoMessage()
        {
            string title = "Add Discount";
            string textMessage = "To add a discount to an item you must first select an item from the list of items in the library";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void RemoveDiscountInfoMessage()
        {
            string title = "Remove Discount";
            string textMessage = "To remove a discount for an item you must first select an item from the list of items in the library";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void RentItemInfoMessage()
        {
            string title = "Rent Item";
            string textMessage = "To rent an item you must first select an item from the list of items in the library";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void ReturnItemInfoMessage()
        {
            string title = "Return Item";
            string textMessage = "To return an item to the library you must first select an item from the list of rented items";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void ApproveUserInfoMessage()
        {
            string title = "Approve User";
            string textMessage = "To approve\\unapprove a user you must first select a user from the list of users";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void DeleteUserInfoMessage()
        {
            string title = "Delete User";
            string textMessage = "To remove a user you must first select a user from the list of users";
            MessageBoxViewInfo(title, textMessage);
        }
        #endregion

        #region Error Messages In Menus
        public static void AddDiscountItemRangeErrorMessage()
        {
            string title = "Range Error";
            string textMessage = "You must enter a value in the range of numbers\nbetween 1 and 100";
            MessageBoxViewError(title, textMessage);
        }

        public static void ReturnItemErrorMessage()
        {
            string title = "User Not Approved";
            string textMessage = "You cannot return an item you did not rent\nOnly the renter can return the item to the library";
            MessageBoxViewError(title, textMessage);
        }

        public static void DeleteUserErrorMessage()
        {
            string title = "Cannot Be Deleted";
            string textMessage = "You cannot delete the user - SuperUser@1991\\AdminUser@1991 because it is a system user";
            MessageBoxViewError(title, textMessage);
        }

        public static void ApproveUserErrorMessage()
        {
            string title = "Cannot Be Unapprove";
            string textMessage = "You cannot unapprove the user - AdminUser@1991 because it is a system administrator";
            MessageBoxViewError(title, textMessage);
        }
        #endregion

        #region Question Message In Menus
        public static bool RemoveItemQuestionMessage()
        {
            string title = "Remove Item";
            string textMessage = "Are you sure you want to delete this item from the library?";
            return MessageBoxViewQuestion(title, textMessage, "Remove");
        }

        public static bool AddDiscountQuestionMessage()
        {
            string title = "Add Discount";
            string textMessage = "Are you sure you want to add the discount to the item?";
            return MessageBoxViewQuestion(title, textMessage, "Add");
        }

        public static bool RemoveDiscountQuestionMessage()
        {
            string title = "Remove Discount";
            string textMessage = "Are you sure you want to remove the item discount?";
            return MessageBoxViewQuestion(title, textMessage, "Remove");
        }

        public static bool RentItemQuestionMessage()
        {
            string title = "Rent Item";
            string textMessage = "Are you sure you want to rent this item?";
            return MessageBoxViewQuestion(title, textMessage, "Rent");
        }

        public static bool ReturItemQuestionMessage()
        {
            string title = "Return Item";
            string textMessage = "Are you sure you want to return the item to the library?";
            return MessageBoxViewQuestion(title, textMessage, "Return");
        }

        public static bool ApproveUserQuestionMessage()
        {
            string title = "Approve\\Unapprove User";
            string textMessage = "Are you sure you want to change the user status??";
            return MessageBoxViewQuestion(title, textMessage, "Remove");
        }

        public static bool DeleteUserQuestionMessage()
        {
            string title = "Delete User";
            string textMessage = "Are you sure you want to delete this user from the library?";
            return MessageBoxViewQuestion(title, textMessage, "Remove");
        }
        #endregion

        #region Info Message In Menus
        public static void RemovedItemMessage()
        {
            string title = "Removed Item";
            string textMessage = "The item has been successfully removed";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void AddedDiscountMessage()
        {
            string title = "Added Discount";
            string textMessage = "The item discount has been successfully added";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void RemovedDiscountMessage()
        {
            string title = "Removed Discount";
            string textMessage = "The discount on the item has been removed";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void RentedItemMessage()
        {
            string title = "Rented Item";
            string textMessage = "The item was rented successfully";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void ReturnedItemMessage()
        {
            string title = "Returned Item";
            string textMessage = "The item has been successfully returned to the library";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void ApprovedOrNotApprovedUserMessage()
        {
            string title = "Approved\\Unapproved User";
            string textMessage = "The user status approved\\unapproved was successfully changed";
            MessageBoxViewInfo(title, textMessage);
        }

        public static void DeletedItemMessage()
        {
            string title = "Deleted User";
            string textMessage = "User deleted successfully";
            MessageBoxViewInfo(title, textMessage);
        }
        #endregion

        #region Pattern Info\Question\Error
        public static void MessageBoxViewInfo(string title, string textMessage)
        {
            string caption = title;
            string messageBoxText = textMessage;
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, buttons, icon);
        }

        public static bool MessageBoxViewQuestion(string title, string textMessage, string strAction)
        {
            string caption = title;
            string messageBoxText = textMessage;
            MessageBoxImage icon;
            MessageBoxButton buttons = MessageBoxButton.YesNo;

            if (strAction == "Remove")
                icon = MessageBoxImage.Warning;
            else
                icon = MessageBoxImage.Question;

            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, buttons, icon);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    return false;

                default: return false;
            }
        }

        public static void MessageBoxViewError(string title, string textMessage)
        {
            string caption = title;
            string messageBoxText = textMessage;
            MessageBoxButton buttons = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, buttons, icon);
        }
        #endregion

    }
}
