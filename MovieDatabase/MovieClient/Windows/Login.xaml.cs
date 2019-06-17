using MovieClient.Services;
using RequestServer.Models;
using System.Linq;
using System.Windows;

namespace MovieClient.Windows
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            CurrWin = this;
        }

        public static Window CurrWin { get; set; }
        public static User User { get; set; }
    }
}
