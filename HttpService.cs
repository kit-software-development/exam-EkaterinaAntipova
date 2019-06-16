using Newtonsoft.Json;
using RequestServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieClient.Services
{
    public static class HttpService
    {
        static HttpService()
        {
            StreamReader reader = new StreamReader("HostInfo.txt");
            var path = reader.ReadLine();
            reader.Close();
            Http = new HttpClient
            {
                BaseAddress = new Uri(path +"api/")
            };
        }
        static HttpClient Http { get; set; }

        public static (bool isSuccess, User user) Login(User user)
        {
            var json = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var request = Http.PostAsync("User/LogIn", json).Result;


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return (false, null);

            var logUs = JsonConvert.DeserializeObject<User>(request.Content.ReadAsStringAsync().Result);

            return (true, logUs);
        }

        public static (bool isSuccess, int Id) Registration(User user)
        {
            var json = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var request = Http.PostAsync("User/Registration", json).Result;


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return (false, 0);

            var logUs = Convert.ToInt32(request.Content.ReadAsStringAsync().Result);

            return (true, logUs);
        }

        public static bool AddFilm(Film film)
        {
            var json = new StringContent(JsonConvert.SerializeObject(film), Encoding.UTF8, "application/json");
            var request = Http.PostAsync("Admin/AddFilm", json).Result;


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return false;
            return true;
        }

        public static (bool isSuccess, List<Film> films) GetFilms()
        {
            var request = Http.GetAsync("User/GetFilms").Result;


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return (false, null);

            var logUs = JsonConvert.DeserializeObject<List<Film>>(request.Content.ReadAsStringAsync().Result);

            return (true, logUs);
        }

        public static async Task<(bool isSuccess, List<Comment> comments)> GetCommentsAsync(int id)
        {
            var request = await Http.GetAsync($"User/GetComments?Id={id}");


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return (false, null);

            var logUs = JsonConvert.DeserializeObject<List<Comment>>(await request.Content.ReadAsStringAsync());

            return (true, logUs);
        }

        public static async Task<(bool isSuccess, Comment comment)> SendCommentAsync(Comment comment)
        {
            var json = new StringContent(JsonConvert.SerializeObject(comment), Encoding.UTF8, "application/json");
            var request = Http.PostAsync("User/AddComment", json).Result;


            if (request.StatusCode != System.Net.HttpStatusCode.OK)
                return (false, null);

            var logUs = JsonConvert.DeserializeObject<Comment>(await request.Content.ReadAsStringAsync());

            return (true, logUs);
        }
    }
}
