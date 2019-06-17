using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RequestServer.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public DateTime Date { get; set; }
        public double Score { get; set; }
        public int ScoreCount { get; set; }
        public string ImagePath { get; set; }

        public virtual List<Comment> Comments { get; set; }
    }
}
