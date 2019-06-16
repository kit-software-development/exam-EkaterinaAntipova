using MovieClient.Other;
using MovieClient.Pages;
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

namespace MovieClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel = new MainWindowViewModel
            {
                Window = this
            };
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            App.Win = this;
            Windows.Login log = new Windows.Login
            {
                WindowStartupLocation = WindowStartupLocation.CenterScreen,
                Visibility = Visibility.Visible
            };
            log.ShowDialog();
            if (log.DialogResult != true)
                Close();
            App.CurrentUser = Windows.Login.User;
            
        }

        public MainWindowViewModel ViewModel { get; set; }

        private void ShowLeftPanel(object sender, MouseButtonEventArgs e)
        {
            if (ViewModel.LeftPanelVisibility == Visibility.Visible)
                ViewModel.LeftPanelVisibility = Visibility.Collapsed;
            else
                ViewModel.LeftPanelVisibility = Visibility.Visible;
        }
    }

    public class MainWindowViewModel : BaseViewModel
    {
        public new MainWindow Window { get; set; }

        private Visibility _leftPanelVisibility = Visibility.Collapsed;
        private string _headerText = "Список фильмов";

        public Visibility LeftPanelVisibility
        {
            get => _leftPanelVisibility;
            set
            {
                _leftPanelVisibility = value;
                OnPropertyChanged("LeftPanelVisibility");
            }
        }

        public string HeaderText
        {
            get => _headerText;
            set
            {
                _headerText = value;
                OnPropertyChanged("HeaderText");
            }
        }

        public BaseCommand AddFilmCommand
        {
            get => new BaseCommand(() =>
            {
                LeftPanelVisibility = Visibility.Collapsed;
                Window.MainFrame.Navigate(new AddMovie());
            });
        }

        public BaseCommand MoviesListCommand
        {
            get => new BaseCommand(() =>
            {
                LeftPanelVisibility = Visibility.Collapsed;
                Window.MainFrame.Navigate(new MoviesList());
            });
        }

        public BaseCommand ExitCommand
        {
            get => new BaseCommand(() =>
            {
                LeftPanelVisibility = Visibility.Collapsed;
                App.Win.Visibility = Visibility.Collapsed;
                Windows.Login log = new Windows.Login
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Visibility = Visibility.Visible
                };
                log.ShowDialog();
                if (log.DialogResult != true)
                {
                    App.Win.Close();
                    return;
                }
                App.CurrentUser = Windows.Login.User;
                App.Win.Visibility = Visibility.Visible;
            });
        }
    }
}
