using System.Windows;

namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для UserAddWindow.xaml
    /// </summary>
    public partial class UserAddWindow : Window
    {
        public UserAddWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.CurrentUser != null)
            {
                var context = ShoeStoreDBEntities.GetContext();
                var newUser = new Users
                {
                    //Ид не нужен если есть identity
                    user_login = LoginTextBox.Text,
                    user_password = PasswordTextBox.Text,
                    fio = FioTextBox.Text
                };
                context.Users.Add(newUser);
                context.SaveChanges();
                MessageBox.Show("Пользователь успешно добавлен");
            }
            else
            {
                MessageBox.Show("Данная операция вам не достуана");
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
