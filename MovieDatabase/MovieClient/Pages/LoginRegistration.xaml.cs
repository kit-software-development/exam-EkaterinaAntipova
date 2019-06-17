using MovieClient.Other;
using MovieClient.Services;
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
    /// Interaction logic for LoginRegistration.xaml
    /// </summary>
    public partial class LoginRegistration : Page
    {
        public LoginRegistration()
        {
            InitializeComponent();
            DataContext = new LoginRegistrationViewModel
            {
                Page = this
            };
        }
    }

    public class LoginRegistrationViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();

        public BaseCommand CancelCommand
        {
            get => new BaseCommand(() =>
            {
                Page.NavigationService.GoBack();
            });
        }

        public BaseCommand RegCommand
        {
            get => new BaseCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(User.NickName)
                || string.IsNullOrWhiteSpace(User.Login)
                || string.IsNullOrWhiteSpace(User.Password))
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }

                var (isSuccess, Id) = HttpService.Registration(User);
                if(isSuccess)
                {
                    User.Id = Id;
                    MessageBox.Show("Регистрация успешна");
                    Page.NavigationService.GoBack();
                }
                else
                {
                    MessageBox.Show("Во время соединения с сервером произошла ошибка");
                }
            });
        }
    }
}
