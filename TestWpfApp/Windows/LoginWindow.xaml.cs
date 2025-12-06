using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using EasyCaptcha.Wpf;


namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string _currentCaptcha;
        private Random _random = new Random();
        public LoginWindow()
        {
            InitializeComponent();
           
            MyCaptcha.CreateCaptcha(Captcha.LetterOption.Alphanumeric, 5);
            LoadCredentials();
        }
       private void LoadCredentials()
        {
            ///<summary>
            ///Надо прописать в App.Config
            /// </summary>
            LoginTextBox.Text = ConfigurationManager.AppSettings["SavedLogin"];
            PasswordTextBox.Text = ConfigurationManager.AppSettings["SavedPassword"];
            RememberMe.IsChecked = ConfigurationManager.AppSettings["RememberMe"] == "true";
        }
        private void SaveCredentials()
        {
            if (RememberMe.IsChecked == true) 
            { 
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                config.AppSettings.Settings["SavedLogin"].Value = LoginTextBox.Text;
                config.AppSettings.Settings["SavedPassword"].Value = PasswordTextBox.Text;
                config.AppSettings.Settings["RememberMe"].Value = "true";
                config.Save();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string answere = CaptchaTextBox.Text;
           
            if ( answere != MyCaptcha.CaptchaText)
            {
                
                CaptchaTextBox.Clear();
                MessageBox.Show("Не верная каптча");
                return;

            }
            else
            {
                string login = LoginTextBox.Text;
                string password = PasswordTextBox.Text;
                var context = ShoeStoreDBEntities.GetContext();
                var user = context.Users.FirstOrDefault(x => x.user_login == login && x.user_password == password);
                if (user != null)
                {
                    ///<summary>
                    /// App.CurrentUser = user.id; задается в App.xaml.cs
                    /// </summary>
                    App.CurrentUser = user.id;
                    var usersWindow = new UsersWindow();
                    SaveCredentials();
                    usersWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль");
                }
            }
        }

        private void GuestLoginButton_Click(object sender, RoutedEventArgs e)
        {
            var usersWindow = new UsersWindow();
            
            usersWindow.Show();
            this.Close();
        }
    }
}
