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
    }
}