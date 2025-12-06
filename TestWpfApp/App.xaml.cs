using System.Windows;

namespace TestWpfApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static int? CurrentUser {  get; set; } = null;
    }
}
