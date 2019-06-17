using Microsoft.AspNetCore.SignalR.Client;
using MovieClient.Other;
using MovieClient.Services;
using RequestServer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for MovieDetail.xaml
    /// </summary>
    public partial class MovieDetail : Page
    {
        public MovieDetail(Film film)
        {
            InitializeComponent();
            DataContext = new MovieDetailViewModel(film)
            {
                Page = this
            };
            App.Win.ViewModel.HeaderText = film.Name;
        }
    }

    public class MovieDetailViewModel : BaseViewModel
    {
        HubConnection hubConnection;
        SynchronizationContext uiContext;

        private ObservableCollection<Comment> _comments;
        private Comment _newComment;

        public MovieDetailViewModel(Film film)
        {
            StreamReader reader = new StreamReader("HostInfo.txt");
            var path = reader.ReadLine();
            reader.Close();

            Film = film;
            NewComment = new Comment
            {
                Date = DateTime.Now,
                FilmId = Film.Id,
                UserId = App.CurrentUser.Id
            };
            Comments = new ObservableCollection<Comment>();
            GetComments();
            hubConnection = new HubConnectionBuilder()
                .WithUrl(path + "comments")
                .Build();
            uiContext = SynchronizationContext.Current;

            hubConnection.On<Comment>("ReceiveComment", (comment) =>
            {
                uiContext.Send(x => Comments.Insert(0, comment), null);
            });

            Connect();
        }

        public async Task Connect()
        {
            try
            {
                await hubConnection.StartAsync();
                await hubConnection.InvokeAsync("Enter", Film.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Empty, $"Ошибка подключения: {ex.Message}");
            }
        }

        public Film Film { get; set; }
        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                _comments = value;
                OnPropertyChanged("Comments");
            }
        }

        public Comment NewComment
        {
            get => _newComment;
            set
            {
                _newComment = value;
                OnPropertyChanged("NewComment");
            }
        }

        public async void GetComments()
        {
            var (isSuccess, comments) = await HttpService.GetCommentsAsync(Film.Id);
            if (isSuccess)
                Comments = new ObservableCollection<Comment>(comments);
        }

        public BaseCommand AddCommentCommand
        {
            get => new BaseCommand(async () =>
            {
                if(string.IsNullOrWhiteSpace(NewComment.Text))
                {
                    MessageBox.Show("Введите текст комментария");
                    return;
                }
                var comm = NewComment;
                NewComment = new Comment
                {
                    Date = DateTime.Now,
                    FilmId = Film.Id,
                    UserId = App.CurrentUser.Id
                };
                await hubConnection.InvokeAsync("AddComment", comm);
            });
        }
    }
}
