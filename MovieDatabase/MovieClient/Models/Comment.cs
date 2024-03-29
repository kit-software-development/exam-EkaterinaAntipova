﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual Film Film { get; set; }
        public int FilmId { get; set; }
        public DateTime Date { get; set; }
    }
}
