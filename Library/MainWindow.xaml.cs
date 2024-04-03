using Library.HelperClasses;
using Library.ManagerClass;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LibraryManager libraryManager;
        public List<Item>? LibraryListOfListViewData { get; set; }
        public List<Item>? RentListOfListViewData { get; set; }
        public List<Users>? UserListOfListViewData { get; set; }

        ListView listViewUsers = new ListView();
        private int currentIdEditItem = 0;
        private string loggedInUsername = "";

        public MainWindow()
        {
            InitializeComponent();

            libraryManager = new LibraryManager();
        }


        #region Welocme Library

        #region Login Page
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            bool connect = PasswordMatchesUsername(tbUsername, pwbPassword);

            if (tbUsername.Text != "" && pwbPassword.Password != "")
            {
                if (connect)
                {
                    UserType userType = libraryManager.UserTypeCheck(tbUsername.Text);
                    bool userApprove = libraryManager.UserApproveCheck(tbUsername.Text);
                    loggedInUsername = tbUsername.Text;

                    if (userType == UserType.Admin)
                        CheckAdministrator(tbUsername, pwbPassword);

                    else if (userType == UserType.Librarian)
                    {
                        if (userApprove)
                        {
                            DisplayLibrarianPage();
                        }
                        else
                            tbkrStatusMessageLogin.Text = ErrorMessages.LibrarianAccountNotApproved();
                    }

                    else
                    {
                        if (userApprove)
                        {
                            DisplayUserPage();
                        }
                        else
                            tbkrStatusMessageLogin.Text = ErrorMessages.UserAccountBlocked();
                    }
                }
                else
                    tbkrStatusMessageLogin.Text = ErrorMessages.WrongLoginError();
            }
            else
                tbkrStatusMessageLogin.Text = ErrorMessages.EmptyFieldsError();
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();

            LoginStackPanel.Visibility = Visibility.Collapsed;
            SignUpStackPanel.Visibility = Visibility.Visible;
        }

        private void btnLoginAsGuest_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();

            LoginStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Visible;

            AuthorStackPanel.Visibility = Visibility.Collapsed;
            tbkRentingItems.Text = "Back To Login";
            btnMoveItemToRent.Visibility = Visibility.Collapsed;
            btnMoveItemToLibrary.Visibility = Visibility.Collapsed;

            ListViewStackPanel.Visibility = Visibility.Visible;
            ListViewRent.Visibility = Visibility.Collapsed;
            tbkRentList.Visibility = Visibility.Collapsed;
            DisplayingUpdatedListView();
        }
        #endregion

        #region SignUp Page
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            string newUser = tbNewUser.Text;
            string password = pwbNewPassword.Password;
            string confirmPassword = pwbConfirmPassword.Password;

            bool fieldsNoEmpty = ValidationForEmptyFields(newUser, password, confirmPassword);
            bool validUsername = ValidationForUsername(newUser);
            bool usernameIsExsit = libraryManager.UserCheck(newUser);

            if (fieldsNoEmpty)
            {
                if (validUsername)
                {
                    if (!usernameIsExsit)
                    {
                        if (password.Length >= 6)
                        {
                            if (password == confirmPassword)
                            {
                                if (rbtnUser.IsChecked == true)
                                {
                                    libraryManager.AddNewUser(newUser, password, UserType.User);
                                    UIMessages.AddUserMessage();
                                }
                                else
                                {
                                    libraryManager.AddNewUser(newUser, password, UserType.Librarian);
                                    UIMessages.AddLibrarianMessage();
                                }
                            }
                            else
                                tbkrStatusMessageSignUp.Text = ErrorMessages.ConfirmPasswordError();
                        }
                        else
                            tbkrStatusMessageSignUp.Text = ErrorMessages.PasswordInvalidError();
                    }
                    else
                        tbkrStatusMessageSignUp.Text = ErrorMessages.UsernameExistsError();
                }
                else
                    tbkrStatusMessageSignUp.Text = ErrorMessages.UsernameInvalidError();
            }
            else
                tbkrStatusMessageSignUp.Text = ErrorMessages.EmptyFieldsError();
        }

        private bool ValidationForEmptyFields(string newUser, string password, string confirmPassword)
        {
            if (newUser == "" || password == "" || confirmPassword == "")
                return false;

            return true;
        }

        private bool ValidationForUsername(string newUsername)
        {
            bool containsLetters = false;
            bool containsNumbers = false;
            for (int i = 0; i < newUsername.Length; i++)
            {
                if (((int)newUsername[i] >= 65 && (int)newUsername[i] <= 90) || ((int)newUsername[i] >= 97 && (int)newUsername[i] <= 122))
                    containsLetters = true;

                if ((int)newUsername[i] >= 48 && (int)newUsername[i] <= 57)
                    containsNumbers = true;
            }

            if (newUsername.Length >= 4 && containsLetters && containsNumbers)
                return true;

            return false;
        }
        #endregion

        #region Back To Login Button
        private void btnBackToLogin_Click(object sender, RoutedEventArgs e)
        {
            SignUpStackPanel.Visibility = Visibility.Collapsed;
            LoginStackPanel.Visibility = Visibility.Visible;

            tbNewUser.Text = "";
            pwbNewPassword.Password = "";
            pwbConfirmPassword.Password = "";
            rbtnUser.IsChecked = true;
            tbkrStatusMessageSignUp.Text = "";
        }

        private void btnBackToLoginFromRenting_Click(object sender, RoutedEventArgs e)
        {
            tbkRentList.Visibility = Visibility.Visible;
            ListViewRent.Visibility = Visibility.Visible;
            ListViewStackPanel.Visibility = Visibility.Collapsed;

            tbkRentingItems.Text = "Renting Items";
            btnMoveItemToRent.Visibility = Visibility.Visible;
            btnMoveItemToLibrary.Visibility = Visibility.Visible;
            AuthorStackPanel.Visibility = Visibility.Visible;

            TotalCashInLibraryStackPanel.Visibility = Visibility.Collapsed;
            LibraryDetailsQuantityStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Collapsed;
            LoginStackPanel.Visibility = Visibility.Visible;
            tbkWelcomeToTheLibrary.Text = "Welcome To The Library";

            loggedInUsername = "";
        }
        #endregion

        #region TextBox And PasswordBox In Welcome Page
        private void tbUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkUsername);
        }

        private void pwbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IsFieldInfo(sender, tbkPassword);
        }

        private void tbNewUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkNewUser);
            ItsWithoutSpaces(sender);
        }

        private void pwbNewPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IsFieldInfo(sender, tbkNewPassword);
        }

        private void pwbConfirmPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            IsFieldInfo(sender, tbkConfirmPassword);
        }
        #endregion

        #region Validation\Display And Clear Fields On Welcome Page 
        private void ItsWithoutSpaces(object sender)
        {
            string str = ((TextBox)sender).Text;

            for (int i = 0; i < str.Length; i++)
            {
                if ((char)(str[i]) == ' ')
                    str = str.Remove(i, 1);
            }

            ((TextBox)sender).Text = str;
            ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
        }

        private void IsFieldInfo(object sender, TextBlock textBlock)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text.Length > 0)
                    textBlock.Visibility = Visibility.Collapsed;
                else
                    textBlock.Visibility = Visibility.Visible;
            }
            else
            {
                if (((PasswordBox)sender).Password.Length > 0)
                    textBlock.Visibility = Visibility.Collapsed;
                else
                    textBlock.Visibility = Visibility.Visible;
            }
        }

        private bool PasswordMatchesUsername(TextBox textBox, PasswordBox passwordBox)
        {
            bool usernameIsExsit = libraryManager.UserCheck(textBox.Text);

            if (usernameIsExsit)
            {
                bool psaawordIsCorrect = libraryManager.PasswordIsCorrect(textBox.Text, passwordBox.Password);

                if (psaawordIsCorrect)
                    return true;
            }

            return false;
        }

        private void DisplayUserPage()
        {
            LoginStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Visible;
            AuthorStackPanel.Visibility = Visibility.Collapsed;
            ListViewStackPanel.Visibility = Visibility.Visible;
            tbkWelcomeToTheLibrary.Text = "Welcome Back User";

            DisplayingUpdatedListView();

            ClearFields();
        }

        private void DisplayLibrarianPage()
        {
            LoginStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Visible;
            ListViewStackPanel.Visibility = Visibility.Visible;
            tbkWelcomeToTheLibrary.Text = "Welcome Back Librarian";

            DisplayingUpdatedListView();

            ClearFields();
        }

        private void CheckAdministrator(TextBox textBox, PasswordBox passwordBox)
        {
            loggedInUsername = "";
            OpenAdminSystemWindow();    
        }

        private void ClearFields()
        {
            tbkrStatusMessageLogin.Text = "";

            bool correctLoginDetails = PasswordMatchesUsername(tbUsername, pwbPassword);

            if (cbKeepMeLoggedIn.IsChecked == true && correctLoginDetails)
                return;

            tbUsername.Text = "";
            pwbPassword.Password = "";
        }
        #endregion

        #region Admin Window
        private void OpenAdminSystemWindow()
        {
            Window adminWindow = new Window();

            adminWindow.Width = 600;
            adminWindow.Height = 600;
            adminWindow.ResizeMode = ResizeMode.NoResize;
            adminWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            Canvas myCanvas = new Canvas();

            string pathAdminWindow = @"Images/Admin.jpg";
            UriKind uriKind = UriKind.Relative;
            BitmapImage bitmap = new BitmapImage();
            ImageBrush imageBrush = new ImageBrush();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(pathAdminWindow, uriKind);
            bitmap.EndInit();

            imageBrush.ImageSource = bitmap;
            myCanvas.Background = imageBrush;

            listViewUsers = CreateListviewUsersForAdminWindow();
            UpdateOfUserList();

            Label labelAdmin = new Label();
            labelAdmin = CreateLabelForAdminWindow();

            Button btnApprove = new Button();
            btnApprove = CreateApproveButtonForAdminWindow();

            Button btnDelete = new Button();
            btnDelete = CreateDeleteButtonForAdminWindow();

            myCanvas.Children.Add(listViewUsers);
            Canvas.SetTop(listViewUsers, 60);
            Canvas.SetLeft(listViewUsers, 90);

            myCanvas.Children.Add(labelAdmin);
            Canvas.SetTop(labelAdmin, 10);
            Canvas.SetLeft(labelAdmin, 195);

            myCanvas.Children.Add(btnApprove);
            Canvas.SetTop(btnApprove, 475);
            Canvas.SetLeft(btnApprove, 166);

            myCanvas.Children.Add(btnDelete);
            Canvas.SetTop(btnDelete, 475);
            Canvas.SetLeft(btnDelete, 300);

            adminWindow.Content = myCanvas;
            adminWindow.Title = "Admin Window";
            adminWindow.ShowDialog();
        }

        private Label CreateLabelForAdminWindow()
        {
            Label labelAdmin = new Label();
            labelAdmin.Content = "User Management";
            labelAdmin.Width = 210;
            labelAdmin.Height = 50;
            labelAdmin.FontSize = 22;
            labelAdmin.FontWeight = FontWeights.Bold;
            labelAdmin.Foreground = new SolidColorBrush(Colors.Orange);

            return labelAdmin;
        }

        private Button CreateApproveButtonForAdminWindow()
        {
            Button btnApprove = new Button();
            btnApprove.Width = 130;
            btnApprove.Height = 30;
            btnApprove.FontWeight = FontWeights.Bold;
            btnApprove.Content = "Approve\\Unapprove";

            BrushConverter bc = new BrushConverter();
            btnApprove.Background = (Brush)bc.ConvertFrom("#FFFFC261")!;

            btnApprove.Click += BtnApprove_Click;

            return btnApprove;
        }

        private Button CreateDeleteButtonForAdminWindow()
        {
            Button btnDelete = new Button();
            btnDelete.Width = 130;
            btnDelete.Height = 30;
            btnDelete.FontWeight = FontWeights.Bold;
            btnDelete.Content = "Delete User";

            BrushConverter bc = new BrushConverter();
            btnDelete.Background = (Brush)bc.ConvertFrom("#FFFFC261")!;

            btnDelete.Click += BtnDelete_Click;

            return btnDelete;
        }

        private void BtnApprove_Click(object sender, RoutedEventArgs e)
        {
            var userCollection = listViewUsers.SelectedItems;

            if (userCollection.Count == 1)
            {
                StringBuilder sbUser = new StringBuilder();

                foreach (var user in userCollection)
                {
                    sbUser.Append(user);
                }

                string strUser = sbUser.ToString();
                int index = strUser.IndexOf(" ");
                string username = strUser.Substring(0, index);

                string adminUser = libraryManager.GetUsernameOfAdmin();

                bool result = UIMessages.ApproveUserQuestionMessage();
                if (result)
                {
                    if (username != adminUser)
                    {
                        libraryManager.ApproveUser(username);
                        UpdateOfUserList();
                        UIMessages.ApprovedOrNotApprovedUserMessage();
                    }
                    else
                        UIMessages.ApproveUserErrorMessage();
                }
            }
            else
                UIMessages.ApproveUserInfoMessage();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var userCollection = listViewUsers.SelectedItems;

            if (userCollection.Count == 1)
            {
                StringBuilder sbUser = new StringBuilder();

                foreach (var user in userCollection)
                {
                    sbUser.Append(user);
                }

                string strUser = sbUser.ToString();
                int index = strUser.IndexOf(" ");
                string username = strUser.Substring(0, index);

                string superUser = libraryManager.GetUsernameOfSuperUser();
                string adminUser = libraryManager.GetUsernameOfAdmin();

                bool result = UIMessages.DeleteUserQuestionMessage();
                if (result)
                {
                    if (username != superUser && username != adminUser)
                    {
                        libraryManager.RemoveUser(username);
                        UpdateOfUserList();
                        UIMessages.DeletedItemMessage();
                    }
                    else
                        UIMessages.DeleteUserErrorMessage();
                }
            }
            else
                UIMessages.DeleteUserInfoMessage();
        }

        private void UpdateOfUserList()
        {
            UserListOfListViewData = new List<Users>();
            UserListOfListViewData = libraryManager.GetUserList();
            listViewUsers.SetBinding(ListView.ItemsSourceProperty, new Binding { Source = UserListOfListViewData });
        }

        private ListView CreateListviewUsersForAdminWindow()
        {
            ListView listViewUsers = new ListView();
            listViewUsers.Width = 420;
            listViewUsers.Height = 400;
            listViewUsers.FontWeight = FontWeights.Bold;

            listViewUsers.Opacity = new double();
            listViewUsers.Opacity = 0.9;

            var itemContainerStyle = new Style(typeof(ListViewItem));
            var horizontalContentAlignmentSetter = new Setter(HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
            itemContainerStyle.Setters.Add(horizontalContentAlignmentSetter);
            listViewUsers.ItemContainerStyle = itemContainerStyle;

            GridView gridView = new GridView();
            listViewUsers.View = gridView;
            gridView.AllowsColumnReorder = false;

            GridViewColumn gridViewColumnUsername = new GridViewColumn();
            gridViewColumnUsername.Width = 130;
            gridViewColumnUsername.Header = "Username";
            gridViewColumnUsername.DisplayMemberBinding = new Binding("Name");
            gridView.Columns.Add(gridViewColumnUsername);

            GridViewColumn gridViewColumnPassword = new GridViewColumn();
            gridViewColumnPassword.Width = 100;
            gridViewColumnPassword.Header = "Password";
            gridViewColumnPassword.DisplayMemberBinding = new Binding("Password");
            gridView.Columns.Add(gridViewColumnPassword);

            GridViewColumn gridViewColumnType = new GridViewColumn();
            gridViewColumnType.Width = 90;
            gridViewColumnType.Header = "Type";
            gridViewColumnType.DisplayMemberBinding = new Binding("UserType");
            gridView.Columns.Add(gridViewColumnType);

            GridViewColumn gridViewColumnApprove = new GridViewColumn();
            gridViewColumnApprove.Width = 90;
            gridViewColumnApprove.Header = "Approved";
            gridViewColumnApprove.DisplayMemberBinding = new Binding("UserApproved");
            gridView.Columns.Add(gridViewColumnApprove);

            return listViewUsers;
        }
        #endregion

        #endregion

        #region Welocme Add\Edit Page

        #region Add\Edit Item Page
        private void btnAddNewItem_Click(object sender, RoutedEventArgs e)
        {
            ListViewStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Collapsed;
            LibraryDetailsQuantityStackPanel.Visibility = Visibility.Collapsed;
            AddItemStackPanel.Visibility = Visibility.Visible;

            tbkWelcomeToTheLibrary.Text = "Add Item Page";
        }

        private void rbtnMagazine_Checked(object sender, RoutedEventArgs e)
        {
            tbAuthor.IsEnabled = false;
            tbAuthor.Text = "";
        }

        private void rbtnBook_Checked(object sender, RoutedEventArgs e)
        {
            tbAuthor.IsEnabled = true;
        }

        private void btnClearFields_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFieldsInAddNewItem();
        }

        private void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string name = tbName.Text;
            string genre = tbGenre.Text;
            string author = tbAuthor.Text;
            string publisher = tbPublisher.Text;
            string price = tbPrice.Text;
            string day = tbDay.Text;
            string month = tbMonth.Text;
            string year = tbYear.Text;

            bool fieldsNoEmpty = ValidationForEmptyFields(name, genre, publisher, price, day, month, year);

            if (!fieldsNoEmpty)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.EmptyFieldsError();
                return;
            }

            bool firstCharacterIsSpace = ValidationForFirstCharacter(name, genre, publisher);

            if (!firstCharacterIsSpace)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.FirstCharacterIsSpace();
                return;
            }

            if (rbtnBook.IsChecked == false && rbtnMagazine.IsChecked == false)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.ItemSelectedError();
                return;
            }

            int dayInt = int.Parse(day);
            int monthInt = int.Parse(month);
            int yearInt = int.Parse(year);

            bool publishDateIsCorrect = ValidationForPublishDate(dayInt, monthInt, yearInt);

            if (!publishDateIsCorrect)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.InvalidRangeOfDateError();
                return;
            }

            DateTime publishDate = libraryManager.CheckPublisheDate(dayInt, monthInt, yearInt);

            if (publishDate == DateTime.MinValue)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.InvalidDateError();
                return;
            }

            if (publishDate > DateTime.Now)
            {
                tbkStatusMessageAddItem.Text = ErrorMessages.FutureDateError();
                return;
            }

            double priceDouble = double.Parse(price);

            if (rbtnBook.IsChecked == true)
            {
                if (author != "")
                {
                    if (author[0] != ' ')
                    {
                        bool validNameitem;
                        if (btnAddItem.Content.ToString() == "Add Item")
                            validNameitem = libraryManager.CheckNameOfNewItem(ItemType.Book, name, author, -1);
                        else
                            validNameitem = libraryManager.CheckNameOfNewItem(ItemType.Book, name, author, currentIdEditItem);

                        if (!validNameitem)
                        {
                            if (btnAddItem.Content.ToString() == "Add Item")
                            {
                                libraryManager.AddItem(ItemType.Book, name, genre, author, publisher, priceDouble, publishDate);
                                tbkStatusMessageAddItem.Text = UIMessages.AddItemMessage();
                            }
                            else
                            {
                                libraryManager.EditeItem(currentIdEditItem, name, genre, author, publisher, priceDouble, publishDate);
                                tbkStatusMessageAddItem.Text = UIMessages.EditeItemMessage();
                            }

                        }
                        else
                            tbkStatusMessageAddItem.Text = ErrorMessages.BookWithSameNameError();
                    }
                    else
                        tbkStatusMessageAddItem.Text = ErrorMessages.FirstCharacterIsSpace();
                }
                else
                    tbkStatusMessageAddItem.Text = ErrorMessages.EmptyFieldsError();
            }
            else
            {
                bool validNameitem;
                if (btnAddItem.Content.ToString() == "Add Item")
                    validNameitem = libraryManager.CheckNameOfNewItem(ItemType.Magazine, name, author, -1);
                else
                    validNameitem = libraryManager.CheckNameOfNewItem(ItemType.Magazine, name, author, currentIdEditItem);

                if (!validNameitem)
                {
                    if (btnAddItem.Content.ToString() == "Add Item")
                    {
                        libraryManager.AddItem(ItemType.Magazine, name, genre, author, publisher, priceDouble, publishDate);
                        tbkStatusMessageAddItem.Text = UIMessages.AddItemMessage();
                    }
                    else
                    {
                        libraryManager.EditeItem(currentIdEditItem, name, genre, author, publisher, priceDouble, publishDate);
                        tbkStatusMessageAddItem.Text = UIMessages.EditeItemMessage();
                    }
                }
                else
                    tbkStatusMessageAddItem.Text = ErrorMessages.MagazineWithSameNameError();
            }
        }

        private void btnBackToLibrary_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFieldsInAddNewItem();
            rbtnBook.IsChecked = false;
            rbtnMagazine.IsChecked = false;
            rbtnBook.IsEnabled = true;
            rbtnMagazine.IsEnabled = true;
            tbkAddNewItem.Text = "Add New Item";
            btnAddItem.Content = "Add Item";

            AddItemStackPanel.Visibility = Visibility.Collapsed;
            MenuStackPanel.Visibility = Visibility.Visible;
            ListViewStackPanel.Visibility = Visibility.Visible;
            LibraryDetailsQuantityStackPanel.Visibility = Visibility.Visible;
            DisplayingUpdatedListView();

            tbkWelcomeToTheLibrary.Text = "Welcome Back Librarian";
        }
        #endregion

        #region Button - Edit Item
        private void btnEditItem_Click(object sender, RoutedEventArgs e)
        {
            List<string> detailsList = new List<string>();
            detailsList = CheckItemToEdit();

            if (detailsList.Count > 0)
            {
                ListViewStackPanel.Visibility = Visibility.Collapsed;
                MenuStackPanel.Visibility = Visibility.Collapsed;
                LibraryDetailsQuantityStackPanel.Visibility = Visibility.Collapsed;
                AddItemStackPanel.Visibility = Visibility.Visible;


                tbkWelcomeToTheLibrary.Text = "Edit Item Page";
                tbkAddNewItem.Text = "  Edit Item  ";
                btnAddItem.Content = "Edit Item";

                UpdateDetailsOfItem(detailsList);
            }
        }

        private List<string> CheckItemToEdit()
        {
            var itemCollection = ListViewLibrary.SelectedItems;

            List<string> detailsList = new List<string>();

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);
                currentIdEditItem = idItem;

                detailsList = libraryManager.ReturnDetailsOfSelectedItem(idItem);

            }
            else
                UIMessages.EditeItemInfoMessage();

            return detailsList;
        }

        private void UpdateDetailsOfItem(List<string> detailsList)
        {
            if (detailsList[0] == ItemType.Book.ToString())
            {
                rbtnBook.IsChecked = true;
                rbtnMagazine.IsEnabled = false;
                tbAuthor.Text = detailsList[8];
            }
            else
            {
                rbtnMagazine.IsChecked = true;
                rbtnBook.IsEnabled = false;
            }

            tbName.Text = detailsList[1];
            tbGenre.Text = detailsList[2];
            tbPublisher.Text = detailsList[3];

            tbPrice.Text = detailsList[4];

            tbDay.Text = detailsList[5];
            tbMonth.Text = detailsList[6];
            tbYear.Text = detailsList[7];
        }
        #endregion

        #region TextBox In Add Item Page
        private void tbName_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkName);
        }

        private void tbGenre_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkGenre);
        }

        private void tbAuthor_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkAuthor);
        }

        private void tbPublisher_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkPublisher);
        }

        private void tbPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkPrice);
            IsDigit(sender);
        }

        private void tbDay_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkDay);
            IsDigit(sender);
        }

        private void tbMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkMonth);
            IsDigit(sender);
        }

        private void tbYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkYear);
            IsDigit(sender);
        }
        #endregion

        #region Validation And Clear Fields On New\Edit Item Page 
        private void IsDigit(object sender)
        {
            string str = ((TextBox)sender).Text;

            for (int i = 0; i < str.Length; i++)
            {
                if (!char.IsDigit(str[i]))
                    str = str.Remove(i, 1);
            }

            ((TextBox)sender).Text = str;
            ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
        }

        private bool ValidationForEmptyFields(string name, string genre, string publisher, string price,
            string day, string month, string year)
        {
            bool emptyFields = (name == "" || genre == "" || publisher == "" || price == "" || day == "" || month == "" || year == "");

            if (emptyFields)
                return false;

            return true;
        }

        private bool ValidationForFirstCharacter(string name, string genre, string publisher)
        {
            bool firstCharacterIsSpace = (name[0] == ' ' || genre[0] == ' ' || publisher[0] == ' ');

            if (firstCharacterIsSpace)
                return false;

            return true;
        }

        private bool ValidationForPublishDate(int day, int month, int year)
        {
            bool publishDateInRange = (day > 0 && day < 32) && (month > 0 && month < 13) && (year >= 2000);

            if (publishDateInRange)
                return true;

            return false;
        }

        private void ClearAllFieldsInAddNewItem()
        {
            tbName.Text = "";
            tbGenre.Text = "";
            tbAuthor.Text = "";
            tbPublisher.Text = "";
            tbPrice.Text = "";

            tbDay.Text = "";
            tbMonth.Text = "";
            tbYear.Text = "";

            tbkStatusMessageAddItem.Text = "Status Message";
        }
        #endregion

        #endregion

        #region Welcome Back Page

        #region Search - Display\Update\Button Clear And Validation
        private void DisplayingSearchResults()
        {
            string type = tbTypeSearch.Text;
            string name = tbNameSearch.Text;
            string genre = tbGenreSearch.Text;
            string author = tbAuthorSearch.Text;
            string publisher = tbPublisherSearch.Text;
            string price = tbPriceSearch.Text;

            List<Item> searchResults = new List<Item>();

            searchResults = libraryManager.Search(type, name, genre, author, publisher, price);

            UpdateOfLibraryListBySearch(searchResults);

            tbkTotalResults.Visibility = Visibility.Visible;
            tbkTotalResults.Text = ($"Results - {searchResults.Count}");
        }

        private void UpdateOfLibraryListBySearch(List<Item> searchResults)
        {
            LibraryListOfListViewData = searchResults;
            ListViewLibrary.ItemsSource = LibraryListOfListViewData;
            DataContext = this;
        }

        private void btnClearFieldsSearch_Click(object sender, RoutedEventArgs e)
        {
            ClearAllFieldsInSearch();
        }

        private void ClearAllFieldsInSearch()
        {
            tbTypeSearch.Text = "";
            tbNameSearch.Text = "";
            tbGenreSearch.Text = "";
            tbAuthorSearch.Text = "";
            tbPublisherSearch.Text = "";
            tbPriceSearch.Text = "";
        }

        private void ClosedVisibilityTotalSearchResults()
        {
            if (tbTypeSearch.Text == "" && tbNameSearch.Text == "" && tbGenreSearch.Text == "" &&
                tbAuthorSearch.Text == "" && tbPublisherSearch.Text == "" && tbPriceSearch.Text == "")

                tbkTotalResults.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region TextBox In Search
        private void tbTypeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkTypeSearch);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }

        private void tbNameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkNameSearch);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }

        private void tbGenreSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkGenreSearch);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }

        private void tbAuthorSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkAuthorSearch);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }

        private void tbPublisherSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkPublisherSearch);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }

        private void tbPriceSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkPriceSearch);
            IsDigit(sender);
            DisplayingSearchResults();
            ClosedVisibilityTotalSearchResults();
        }
        #endregion

        #region ListView - Update And Displaying 
        private void UpdateOfLibraryList()
        {
            ClearAllFieldsInSearch();

            LibraryListOfListViewData = new List<Item>();
            LibraryListOfListViewData = libraryManager.GetAllItemsInLibrary();
            DataContext = this;
        }

        private void UpdateOfRentList()
        {
            libraryManager.LateOnReturn();

            RentListOfListViewData = new List<Item>();
            RentListOfListViewData = libraryManager.GetAllItemsInRent();
            DataContext = this;
        }

        private void DisplayingUpdatedListView()
        {
            ListViewLibrary.ItemsSource = null;
            UpdateOfLibraryList();
            ListViewLibrary.ItemsSource = LibraryListOfListViewData;

            ListViewRent.ItemsSource = null;
            UpdateOfRentList();
            ListViewRent.ItemsSource = RentListOfListViewData;

            DisplayingTotalItemsInLibrary();
            DisplayingUsernameAndTotalCash(loggedInUsername);
        }

        private void ListViewLibrary_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewRent.ItemsSource = null;
            UpdateOfRentList();
            ListViewRent.ItemsSource = RentListOfListViewData;
        }

        private void ListViewRent_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListViewLibrary.ItemsSource = null;
            UpdateOfLibraryList();
            ListViewLibrary.ItemsSource = LibraryListOfListViewData;
        }
        #endregion

        #region Buttons - RemoveItem\AddDiscount\RemoveDiscount
        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            var itemCollection = ListViewLibrary.SelectedItems;

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);

                bool result = UIMessages.RemoveItemQuestionMessage();
                if (result)
                {
                    libraryManager.RemoveItem(idItem);
                    DisplayingUpdatedListView();
                    UIMessages.RemovedItemMessage();
                }
            }
            else
                UIMessages.RemoveItemInfoMessage();
        }

        private void btnAddDiscount_Click(object sender, RoutedEventArgs e)
        {
            var itemCollection = ListViewLibrary.SelectedItems;

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);

                string percentageDiscount = tbAddDiscount.Text;

                if (ValidationEmptyFieldAndRangeNumber(percentageDiscount))
                {
                    int percentageDiscountInt = int.Parse(percentageDiscount);

                    bool result = UIMessages.AddDiscountQuestionMessage();
                    if (result)
                    {
                        libraryManager.AddDiscount(idItem, percentageDiscountInt);
                        DisplayingUpdatedListView();
                        UIMessages.AddedDiscountMessage();
                    }
                }
                else
                    UIMessages.AddDiscountItemRangeErrorMessage();
            }
            else
                UIMessages.AddDiscountItemInfoMessage();

            tbAddDiscount.Text = "";
        }

        private void btnRemoveDiscount_Click(object sender, RoutedEventArgs e)
        {
            var itemCollection = ListViewLibrary.SelectedItems;

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);

                bool result = UIMessages.RemoveDiscountQuestionMessage();
                if (result)
                {
                    libraryManager.RemoveDiscount(idItem);
                    DisplayingUpdatedListView();
                    UIMessages.RemovedDiscountMessage();
                }

            }
            else
                UIMessages.RemoveDiscountInfoMessage();

            tbAddDiscount.Text = "";
        }
        #endregion

        #region Buttons - Rent\Return Items
        private void btnMoveItemToRent_Click(object sender, RoutedEventArgs e)
        {
            var itemCollection = ListViewLibrary.SelectedItems;

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);

                bool result = UIMessages.RentItemQuestionMessage();
                if (result)
                {
                    libraryManager.RentItem(idItem, loggedInUsername);
                    DisplayingUpdatedListView();
                    UIMessages.RentedItemMessage();
                }
            }
            else
                UIMessages.RentItemInfoMessage();
        }

        private void btnMoveItemToLibrary_Click(object sender, RoutedEventArgs e)
        {
            var itemCollection = ListViewRent.SelectedItems;

            if (itemCollection.Count == 1)
            {
                StringBuilder sbItem = new StringBuilder();

                foreach (var item in itemCollection)
                {
                    sbItem.Append(item);
                }

                string strItem = sbItem.ToString();
                int index = strItem.IndexOf(" ");
                strItem = strItem.Substring(0, index);

                int idItem = int.Parse(strItem);
                string renter = libraryManager.GetRenterOfItem(idItem);

                string superUser = libraryManager.GetUsernameOfSuperUser();

                if (renter == loggedInUsername || loggedInUsername == superUser)
                {
                    bool result = UIMessages.ReturItemQuestionMessage();
                    if (result)
                    {
                        libraryManager.ReturnItem(idItem);
                        DisplayingUpdatedListView();
                        UIMessages.ReturnedItemMessage();
                    }
                }
                else
                    UIMessages.ReturnItemErrorMessage();
            }
            else
                UIMessages.ReturnItemInfoMessage();
        }
        #endregion

        #region Validation And TextBox Of Discount On Welcome Back
        private bool ValidationEmptyFieldAndRangeNumber(string percentageDiscount)
        {
            if (percentageDiscount != "")
            {
                int percentageDiscountInt = int.Parse(percentageDiscount);

                if (percentageDiscountInt > 0 && percentageDiscountInt <= 100)
                    return true;

                else
                    return false;
            }
            else
                return false;
        }

        private void tbAddDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsFieldInfo(sender, tbkAddDiscount);
            IsDigit(sender);
        }

        private void tbAddDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!btnAddDiscount.IsMouseOver)
                tbAddDiscount.Text = "";
        }
        #endregion

        #endregion

        #region General Settings
        private void DisplayingUsernameAndTotalCash(string username)
        {
            bool userIsExists = libraryManager.UserCheck(username);

            if (!userIsExists)
                TotalCashInLibraryStackPanel.Visibility = Visibility.Collapsed;

            else
            {
                UserType userType = libraryManager.UserTypeCheck(username);

                if (userType == UserType.User)
                    tbkTotalCash.Visibility = Visibility.Collapsed;

                else if (userType == UserType.Librarian)
                    tbkTotalCash.Visibility = Visibility.Visible;

                TotalCashInLibraryStackPanel.Visibility = Visibility.Visible;
            }

            double totalCash = libraryManager.LibraryCashBox;
            string totalCashConvert = "$" + totalCash.ToString("F");

            tbkLoggedInUsername.Text = $"Username: {username}";
            tbkTotalCash.Text = $"Total Cash In The Library - {totalCashConvert}";
        }

        private void DisplayingTotalItemsInLibrary()
        {
            int totalItemsInLibrary = libraryManager.GetCountOfAllItemInLibraray();
            int totalBook = libraryManager.GetCountOfAllBookInLibraray();
            int totalMagazine = totalItemsInLibrary - totalBook;
            int totalItemsAviAvailable = libraryManager.GetCountOfAllItemsAvailable();
            int totalItemsRented = totalItemsInLibrary - totalItemsAviAvailable;

            LibraryDetailsQuantityStackPanel.Visibility = Visibility.Visible;

            tbkTotalItems.Text = $"Total Items In The Library - {totalItemsInLibrary}";
            tbkTotalItemsOfEachTypeBook.Text = $"Book - {totalBook} | Magazine - {totalMagazine}";
            tbkItemAvailable.Text = $"Items Are Available - {totalItemsAviAvailable}";
            tbkItemRent.Text = $"Items Rented - {totalItemsRented}";
        }

        private void Win1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            libraryManager.AddToJsonFile();
        }
        #endregion

    }
}