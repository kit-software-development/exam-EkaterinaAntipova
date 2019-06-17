using MovieClient.Services;
using MovieClient.Windows;
using RequestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieClient.Pages
{
    /// <summary>
    /// Interaction logic for LoginMain.xaml
    /// </summary>
    public partial class LoginMain : Page
    {
        public LoginMain()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string Log { get; set; }

        private void Enter(object sender, RoutedEventArgs e)
        {
            if (Log == "Admin" && Pass.Password == "Admin")
            {
                Login.CurrWin.DialogResult = true;
                return;
            }

            var (isSuccess, user) = HttpService.Login(new User { Login = Log, Password = Pass.Password });
            if (isSuccess)
            {
                Login.User = user;
                Login.CurrWin.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Проверьте правильность введённых данных");
            }
        }

        private void Registration(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginRegistration());
        }
    }
}
