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
    /// Interaction logic for MoviesList.xaml
    /// </summary>
    public partial class MoviesList : Page
    {
        public MoviesList()
        {
            InitializeComponent();
            App.Win.ViewModel.HeaderText = "Список фильмов";
            DataContext = new MoviesListViewModel
            {
                Page = this
            };
        }

        public class MoviesListViewModel : BaseViewModel
        {
            private List<Film> _films;
            private string _searchValue;

            public MoviesListViewModel()
            {
                var (isSuccess, films) = HttpService.GetFilms();
                if (isSuccess)
                    AllFilms = films;
                FilterFilms();
            }

            public List<Film> AllFilms { get; set; }

            public List<Film> Films
            {
                get => _films;
                set
                {
                    _films = value;
                    OnPropertyChanged("Films");
                }
            }
            public Film SelectedFilm
            {
                set
                {
                    if(value != null)
                        Page.NavigationService.Navigate(new MovieDetail(value));
                }
            }

            public BaseCommand RefreshCommand
            {
                get => new BaseCommand(() =>
                {
                    var (isSuccess, films) = HttpService.GetFilms();
                    if (isSuccess)
                        AllFilms = films;
                    FilterFilms();
                });
            }

            public string SearchValue
            {
                get => _searchValue;
                set
                {
                    _searchValue = value;
                    FilterFilms();
                }
            }

            public void FilterFilms()
            {
                var search = SearchValue == null ? string.Empty : SearchValue.ToLower();
                Films = AllFilms.Where(x => string.IsNullOrEmpty(search)
                || x.Date.Year.ToString() == search
                || x.Name.ToLower().Contains(search)
                || x.Description.ToLower().Contains(search)
                || x.Country.ToLower() == search).ToList();
            }
        }
    }
}
