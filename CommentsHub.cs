using Microsoft.AspNetCore.SignalR;
using RequestServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Hubs
{
    public class CommentsHub : Hub
    {
        public async Task Enter(string filmId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, filmId);
        }

        public async Task AddComment(Comment comment)
        {
            using(var dbHelper = new DbModel())
            {
                dbHelper.Comments.Add(comment);
                dbHelper.SaveChanges();
                comment.User = new User
                {
                    NickName = dbHelper.Users.First(x => x.Id == comment.UserId).NickName
                };
            }
            await Clients.Group(comment.FilmId.ToString()).SendAsync("ReceiveComment", comment);
        }
    }
}
