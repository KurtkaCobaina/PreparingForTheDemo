using System.Windows;

namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для UserEditWindow.xaml
    /// </summary>
    public partial class UserEditWindow : Window
    {
        private static Users _curentUser = new Users();
        public UserEditWindow(Users selectedUser)
        {
            InitializeComponent();
            if (selectedUser == null)
            {
                return;
            }
            _curentUser = selectedUser;
            DataContext = _curentUser;

        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                ShoeStoreDBEntities.GetContext().SaveChanges();
                MessageBox.Show("изменения сохранились");
            }
            else
            {
                MessageBox.Show("Вы не можете изменить пользователя");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var userWindow = new UsersWindow();
            userWindow.Show();
            this.Close();
        }
    }
}
