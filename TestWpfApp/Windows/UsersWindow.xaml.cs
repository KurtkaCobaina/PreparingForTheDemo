using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для UsersWindow.xaml
    /// </summary>
    public partial class UsersWindow : Window
    {
        public UsersWindow()
        {
            InitializeComponent();
            UsersDataGrid.ItemsSource = ShoeStoreDBEntities.GetContext().Users.ToList();
            UsersDataGrid.ItemsSource = ShoeStoreDBEntities.GetContext().Users.ToList().OrderBy(x => x.id);

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addUserWindow = new UserAddWindow();
            addUserWindow.Show();
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var item = (Users)UsersDataGrid.SelectedItem;
            new UserEditWindow(item).Show();
            this.Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                var removingUser = UsersDataGrid.SelectedItems.Cast<Users>().ToList();
                ShoeStoreDBEntities.GetContext().Users.RemoveRange(removingUser);
                ShoeStoreDBEntities.GetContext().SaveChanges();
                UsersDataGrid.ItemsSource = ShoeStoreDBEntities.GetContext().Users.ToList();
                UsersDataGrid.ItemsSource = ShoeStoreDBEntities.GetContext().Users.ToList().OrderBy(x => x.id);
            }
            else
            {
                MessageBox.Show("Эта функция вам не доступна");
            }
        }

        private void FiltersChanges(object sender, SelectionChangedEventArgs e)
        {
            var context = ShoeStoreDBEntities.GetContext();
            var users = context.Users.ToList();
            string selectedFilter = (FiltersComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            if (selectedFilter == "Все")
            {
                UsersDataGrid.ItemsSource = users;
            }
            else if(selectedFilter == "Администратор")
            {
                users = users.Where( u => u.Users_Roles.Any(ur => ur.Roles.title == "Администратор")).ToList();
                UsersDataGrid.ItemsSource = users;
            }
            else if (selectedFilter == "Менеджер")
            {
                users = users.Where(u => u.Users_Roles.Any(ur => ur.Roles.title == "Менеджер")).ToList();
                UsersDataGrid.ItemsSource = users;
            }
            else if (selectedFilter == "Авторизированный клиент")
            {
                users = users.Where(u => u.Users_Roles.Any(ur => ur.Roles.title == "Авторизированный клиент")).ToList();
                UsersDataGrid.ItemsSource = users;
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                App.CurrentUser = null;
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        private void SearchResults(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTextBox.Text.ToLower();
            var context = ShoeStoreDBEntities.GetContext();
            var users = context.Users.ToList();
            if (string.IsNullOrWhiteSpace(searchText))
            {
                UsersDataGrid.ItemsSource = ShoeStoreDBEntities.GetContext().Users.ToList();
            }
            else
            {
                var filteredUser = users.Where( u => u.user_login.ToLower().Contains(searchText.ToLower())).OrderBy(x => x.id);
                UsersDataGrid.ItemsSource = filteredUser;
            }
        }
    }
}