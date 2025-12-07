using System.Linq;
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
            RoleCombobox.ItemsSource = ShoeStoreDBEntities.GetContext().Roles.ToList();
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
                var selectedRole = (Roles) RoleCombobox.SelectedItem;
                var maxid = context.Users.Any() ? context.Users.Max(u => u.id) : 0;
                var newId = maxid + 1;
                var userRoles = new Users_Roles { id = newId , role_id = selectedRole.id};
                context.Users_Roles.Add(userRoles);
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
