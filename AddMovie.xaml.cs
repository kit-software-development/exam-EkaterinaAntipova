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
    /// Interaction logic for AddMovie.xaml
    /// </summary>
    public partial class AddMovie : Page
    {
        public AddMovie()
        {
            InitializeComponent();
            DataContext = new AddMovieViewModel
            {
                Page = this
            };
            App.Win.ViewModel.HeaderText = "Добавление фильма";
        }
    }

    public class AddMovieViewModel : BaseViewModel
    {
        public AddMovieViewModel()
        {
            Film = new Film()
            {
                Date = DateTime.Now
            };
        }
        public Film Film { get; set; }

        public BaseCommand CancelCommand
        {
            get => new BaseCommand(() =>
            {
                if (Page.NavigationService.CanGoBack)
                    Page.NavigationService.GoBack();
                else
                    Page.NavigationService.Navigate(new MoviesList());
            });
        }
        public BaseCommand AddFilmCommand
        {
            get => new BaseCommand(() =>
            {
                if (!IsModelValid(Film))
                {
                    MessageBox.Show("Не все поля заполнены");
                    return;
                }
                if(HttpService.AddFilm(Film))
                {
                    MessageBox.Show("Фильм успешно добавлен");
                    if (Page.NavigationService.CanGoBack)
                        Page.NavigationService.GoBack();
                    else
                        Page.NavigationService.Navigate(new MoviesList());
                }
                else
                {
                    MessageBox.Show("Во время соединения с сервером произошла ошибка");
                }
            });
        }

        private bool IsModelValid(Film film)
        {
            return !(string.IsNullOrEmpty(film.Description)
                || string.IsNullOrEmpty(film.Country)
                || string.IsNullOrEmpty(film.Name));
        }
    }
}
